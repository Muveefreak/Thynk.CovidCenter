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
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
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
        private readonly IConfiguration _configuration;
        private readonly ICache _cache;
        private const string AllAvailDateCacheConstant = "COVID_CENTER_ALL_AVAIL_DATES";
        private const string AvailDateCacheConstant = "COVID_CENTER_AVAIL_DATES";

        public AvailableDatesService(IMapper mapper,
            IDBCommandRepository<AvailableDate> availableDatesCommandRepository,
            IDBQueryRepository<AvailableDate> availableDatesQueryRepository,
            ICache cache,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _availableDatesCommandRepository = availableDatesCommandRepository;
            _availableDatesQueryRepository = availableDatesQueryRepository;
            _cache = cache;
            _mapper = mapper;
        }
        public async Task<BaseResponse> CreateDates(List<CreateAvailableDatesRequest> request)
        {
            var availableDatesList = _mapper.Map<List<AvailableDate>>(request);
            availableDatesList.ForEach(x => x.Available = true);

            await _cache.RemoveKeyAsync($"{AvailDateCacheConstant}");

            await _availableDatesCommandRepository.AddRangeAsync(availableDatesList);
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
                await _cache.SetValueAsync($"{AllAvailDateCacheConstant}", JsonConvert.SerializeObject(allAvailableDates), 3600);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public async Task<AvailableDatesResponseModel> GetDates(Guid locationId)
        {
            List<AvailableDate> availableDates = new();
            List<AvailableDatesDTO> availableDatesDTO = new();

            string allAvailableDatesFromCache = await _cache.GetValueAsync($"{AllAvailDateCacheConstant}");

            if (!string.IsNullOrEmpty(allAvailableDatesFromCache))
            {
                availableDatesDTO = _mapper.Map<List<AvailableDatesDTO>>(allAvailableDatesFromCache);
                var availableDatesFilteredDTO = availableDatesDTO.Where(x => x.LocationId == locationId && x.DateAvailable >= DateTime.UtcNow).ToList();

                return new AvailableDatesResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = availableDatesFilteredDTO
                };
            }

            availableDates = (await _availableDatesQueryRepository.GetByAsync(x => x.ID == locationId && x.DateAvailable >= DateTime.UtcNow)).ToList();

            if (availableDates.Any())
            {
                availableDatesDTO = _mapper.Map<List<AvailableDatesDTO>>(availableDates);

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

            string allAvailableDatesFromCache = await _cache.GetValueAsync($"{AllAvailDateCacheConstant}");

            if (!string.IsNullOrEmpty(allAvailableDatesFromCache))
            {
                availableDatesDTO = _mapper.Map<List<AvailableDatesDTO>>(allAvailableDatesFromCache);
                var availableDatesFilteredDTO = availableDatesDTO.Where(x => x.DateAvailable >= DateTime.UtcNow).ToList();

                return new AvailableDatesResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = availableDatesFilteredDTO
                };
            }

            availableDates = (await _availableDatesQueryRepository.GetByAsync(x => x.DateAvailable >= DateTime.UtcNow)).ToList();

            if (availableDates.Any())
            {
                availableDatesDTO = _mapper.Map<List<AvailableDatesDTO>>(availableDates);

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
