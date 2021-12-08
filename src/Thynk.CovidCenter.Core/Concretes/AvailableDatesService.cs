using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.Constants;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.Helpers;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using Thynk.CovidCenter.Data.Enums;
using Thynk.CovidCenter.Data.Models;
using Thynk.CovidCenter.Repository.Cache;
using Thynk.CovidCenter.Repository.Commands.Interfaces;
using Thynk.CovidCenter.Repository.Queries.Interfaces;

namespace Thynk.CovidCenter.Core.Concretes
{
    public class AvailableDatesService : IAvailableDatesService
    {
        private readonly IMapper _mapper;
        private readonly IDBCommandRepository<AvailableDate> _availableDatesCommandRepository;
        private readonly IDBQueryRepository<AvailableDate> _availableDatesQueryRepository;
        private readonly IDBQueryRepository<ApplicationUser> _applicationUserQueryRepository;
        private readonly IConfiguration _configuration;
        private readonly ICache _cache;
        private readonly IUtilities _utilities;

        public AvailableDatesService(IMapper mapper,
            IDBCommandRepository<AvailableDate> availableDatesCommandRepository,
            IDBQueryRepository<AvailableDate> availableDatesQueryRepository,
            IDBQueryRepository<ApplicationUser> applicationUserQueryRepository,
            ICache cache,
            IConfiguration configuration,
            IUtilities utilities)
        {
            _configuration = configuration;
            _availableDatesCommandRepository = availableDatesCommandRepository;
            _availableDatesQueryRepository = availableDatesQueryRepository;
            _applicationUserQueryRepository = applicationUserQueryRepository;
            _cache = cache;
            _mapper = mapper;
            _utilities = utilities;
        }
        public async Task<BaseResponse> CreateDates(CreateAvailableDatesRequest request)
        {
            ApplicationUser user = await _utilities.GetUser(request.ApplicationUserId);
            if (user.UserRole != UserRole.Administrator)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.OnlyAdministrator,
                    Status = false
                };
            }
            Location location = await _utilities.GetLocation(request.LocationId);
            if (location == null)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.NoLocationRecordFound,
                    Status = false
                };
            }

            var alreadyExists = await _availableDatesQueryRepository.IsExistAsync(x => x.LocationId == request.LocationId && x.DateAvailable.Date == Convert.ToDateTime(request.DateAvailable).Date);
            if(alreadyExists)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.NoDuplicateDatesPerLocation,
                    Status = false
                };
            }

            var availableDate = _mapper.Map<AvailableDate>(request);
            availableDate.Available = true;
            availableDate.DateAvailable = availableDate.DateAvailable.Date;

            await _cache.RemoveKeyAsync($"{CacheConstants.AllAvailDateCacheConstant}");

            await _availableDatesCommandRepository.AddAsync(availableDate);
            await _availableDatesCommandRepository.SaveAsync();
            await CacheAllAvailDates();

            return new BaseResponse
            {
                Message = ResponseMessages.Success,
                Status = true
            };
        }

        private async Task CacheAllAvailDates()
        {
            List<AvailableDate> allAvailableDates = (await _availableDatesQueryRepository.GetAllAsync()).ToList();
            try
            {
                await _cache.SetValueAsync($"{CacheConstants.AllAvailDateCacheConstant}", JsonConvert.SerializeObject(allAvailableDates), 3600);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public async Task<AvailableDatesResponseModel> GetDatesByLocation(Guid locationId)
        {
            List<AvailableDate> availableDates = new();
            List<AvailableDatesDTO> availableDatesDTO = new();

            Location location = await _utilities.GetLocation(locationId);
            if (location == null)
            {
                return new AvailableDatesResponseModel
                {
                    Message = ResponseMessages.NoLocationRecordFound,
                    Status = false
                };
            }

            string allAvailableDatesFromCache = await _cache.GetValueAsync($"{CacheConstants.AllAvailDateCacheConstant}");

            if (!string.IsNullOrEmpty(allAvailableDatesFromCache))
            {
                availableDatesDTO = _mapper.Map<List<AvailableDatesDTO>>(JsonConvert.DeserializeObject<List<AvailableDate>>(allAvailableDatesFromCache));
                var availableDatesFilteredDTO = availableDatesDTO.Where(x => x.LocationId == locationId && x.DateAvailable >= DateTime.UtcNow.Date && x.AvailableSlots > 0).ToList();

                return new AvailableDatesResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = availableDatesFilteredDTO
                };
            }

            availableDates = (await _availableDatesQueryRepository.GetByAsync(x => x.LocationId == locationId && x.DateAvailable >= DateTime.UtcNow.Date && x.AvailableSlots > 0)).ToList();

            if (availableDates.Any())
            {
                availableDatesDTO = _mapper.Map<List<AvailableDatesDTO>>(availableDates);
                await CacheAllAvailDates();

                return new AvailableDatesResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = availableDatesDTO
                };
            }

            return new AvailableDatesResponseModel
            {
                Message = ResponseMessages.NoAvailableDatesRecordFound
            };
        }

        public async Task<AvailableDatesResponseModel> GetAllDates()
        {
            List<AvailableDate> availableDates = new();
            List<AvailableDatesDTO> availableDatesDTO = new();

            string allAvailableDatesFromCache = await _cache.GetValueAsync($"{CacheConstants.AllAvailDateCacheConstant}");

            if (!string.IsNullOrEmpty(allAvailableDatesFromCache))
            {
                availableDatesDTO = _mapper.Map<List<AvailableDatesDTO>>(JsonConvert.DeserializeObject<List<AvailableDate>>(allAvailableDatesFromCache));
                var availableDatesFilteredDTO = availableDatesDTO.Where(x => x.DateAvailable >= DateTime.UtcNow.Date).ToList();

                return new AvailableDatesResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = availableDatesFilteredDTO
                };
            }

            availableDates = (await _availableDatesQueryRepository.GetByAsync(x => x.DateAvailable >= DateTime.UtcNow.Date)).ToList();

            if (availableDates.Any())
            {
                availableDatesDTO = _mapper.Map<List<AvailableDatesDTO>>(availableDates);
                await CacheAllAvailDates();

                return new AvailableDatesResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = availableDatesDTO
                };
            }
            return new AvailableDatesResponseModel
            {
                Message = ResponseMessages.NoAvailableDatesRecordFound
            };
        }
    }
}
