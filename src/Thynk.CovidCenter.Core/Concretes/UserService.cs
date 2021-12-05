using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.Constants;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using Thynk.CovidCenter.Data.Models;
using Thynk.CovidCenter.Repository.Cache;
using Thynk.CovidCenter.Repository.Commands.Interfaces;
using Thynk.CovidCenter.Repository.Queries.Interfaces;

namespace Thynk.CovidCenter.Core.Concretes
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IDBCommandRepository<ApplicationUser> _userCommandRepository;
        private readonly IDBQueryRepository<ApplicationUser> _userQueryRepository;
        private readonly IConfiguration _configuration;
        private readonly ICache _cache;
        private const string UserCacheConstant = "COVID_CENTER_USER";
        private const string UsersCacheConstant = "COVID_CENTER_USERS";

        public UserService(IMapper mapper,
            IDBCommandRepository<ApplicationUser> userCommandRepository,
            IDBQueryRepository<ApplicationUser> userQueryRepository,
            ICache cache,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _cache = cache;
            _mapper = mapper;
        }
        public async Task<BaseResponse> CreateUser(CreateUserRequest request)
        {
            var userEntity = _mapper.Map<ApplicationUser>(request);

            byte[] passwordHash, passwordSalt;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            }
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;

            await _cache.RemoveKeyAsync($"{UsersCacheConstant}");

            await _userCommandRepository.AddAsync(userEntity);
            await _userCommandRepository.SaveAsync();
            await CacheUsers();

            return new BaseResponse
            {
                Message = ResponseMessages.Success,
                Status = true
            };
        }

        public async Task<GenericResponse<UserDTO>> GetUser(Guid userId)
        {
            ApplicationUser user = new();
            UserDTO userDTO = new();

            string userFromCache = await _cache.GetValueAsync($"{UserCacheConstant}_{Convert.ToString(userId)}");

            if (!string.IsNullOrEmpty(userFromCache))
            {
                user = JsonConvert.DeserializeObject<ApplicationUser>(userFromCache);

                userDTO = _mapper.Map<UserDTO>(user);
                return new GenericResponse<UserDTO>
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = userDTO
                };
            }

            user = await _userQueryRepository.GetByDefaultAsync(x => x.ID == userId);

            if (user != null)
            {
                await _cache.SetValueAsync($"{UserCacheConstant}_{Convert.ToString(userId)}", JsonConvert.SerializeObject(user), 3600);
                userDTO = _mapper.Map<UserDTO>(user);

                return new GenericResponse<UserDTO>
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = userDTO
                };
            }
            return new GenericResponse<UserDTO>
            {
                Message = ResponseMessages.NoUserRecordFound
            };
        }

        public async Task<UserResponseModel> GetUsers()
        {
            List<ApplicationUser> users = new();
            List<UserDTO> usersDTO = new();

            string usersFromCache = await _cache.GetValueAsync($"{UsersCacheConstant}");

            if (!string.IsNullOrEmpty(usersFromCache))
            {
                users = JsonConvert.DeserializeObject<List<ApplicationUser>>(usersFromCache);

                usersDTO = _mapper.Map<List<UserDTO>>(users);
                return new UserResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = usersDTO
                };
            }

            users = (await _userQueryRepository.GetAllAsync()).ToList();

            if (users.Any())
            {
                usersDTO = _mapper.Map<List<UserDTO>>(users);

                return new UserResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = usersDTO
                };
            }
            return new UserResponseModel
            {
                Message = ResponseMessages.NoUserRecordFound
            };
        }

        private async Task CacheUsers()
        {
            List<ApplicationUser> users = (await _userQueryRepository.GetAllAsync()).ToList();
            try
            {
                await _cache.SetValueAsync($"{UsersCacheConstant}", JsonConvert.SerializeObject(users), 3600);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
