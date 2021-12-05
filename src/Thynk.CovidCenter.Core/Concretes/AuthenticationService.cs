using AutoMapper;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.Constants;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using Thynk.CovidCenter.Data.Models;
using Thynk.CovidCenter.Repository.Commands.Interfaces;
using Thynk.CovidCenter.Repository.Queries.Interfaces;

namespace Thynk.CovidCenter.Core.Concretes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDBQueryRepository<ApplicationUser> _userQueryRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public AuthenticationService(IMapper mapper,
            IDBQueryRepository<ApplicationUser> userQueryRepository,
            IPasswordService passwordService)
        {
            _userQueryRepository = userQueryRepository;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<GenericResponse<UserDTO>> Authenticate(AuthenticationRequest auth)
        {
            var client = await _userQueryRepository.GetByDefaultAsync(x => x.Email == auth.Email);

            if (client == null) return new GenericResponse<UserDTO> { Status = false, Message = ResponseMessages.NoUserRecordFound };

            if (!_passwordService.PasswordCheck(auth.Password, client.PasswordSalt, client.PasswordHash)) return new GenericResponse<UserDTO> { Status = false, Message = ResponseMessages.InvalidCredentials };

            var userDTO = _mapper.Map<UserDTO>(client);

            return new GenericResponse<UserDTO>
            {
                Data = userDTO,
                Status = true,
                Message = ResponseMessages.Success
            };
        }
    }
}
