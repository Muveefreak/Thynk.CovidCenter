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
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly IDBCommandRepository<Location> _locationCommandRepository;
        private readonly IDBQueryRepository<Location> _locationQueryRepository;
        private readonly IConfiguration _configuration;
        private readonly ICache _cache;
        private const string LocationsCacheConstant = "COVID_CENTER_LOCATIONS";
        private const string LocationCacheConstant = "COVID_CENTER_LOCATION";

        public LocationService(IMapper mapper,
            IDBCommandRepository<Location> locationCommandRepository,
            IDBQueryRepository<Location> locationQueryRepository,
            ICache cache,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _locationCommandRepository = locationCommandRepository;
            _locationQueryRepository = locationQueryRepository;
            _cache = cache;
            _mapper = mapper;
        }
        public async Task<BaseResponse> CreateLocation(CreateLocationRequest request)
        {
            var locationEntity = _mapper.Map<Location>(request);

            await _cache.RemoveKeyAsync($"{LocationsCacheConstant}");

            await _locationCommandRepository.AddAsync(locationEntity);
            await _locationCommandRepository.SaveAsync();
            await CacheLocations();

            return new BaseResponse
            {
                Message = ResponseMessages.Success,
                Status = true
            };
        }

        private async Task CacheLocations()
        {
            List<Location> locations = (await _locationQueryRepository.GetAllAsync()).ToList();
            try
            {
                await _cache.SetValueAsync($"{LocationsCacheConstant}", JsonConvert.SerializeObject(LocationsCacheConstant), 3600);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public async Task<GenericResponse<LocationDTO>> GetLocation(Guid locationId)
        {
            Location location = new();
            LocationDTO locationDTO = new();

            string locationFromCache = await _cache.GetValueAsync($"{LocationCacheConstant}_{Convert.ToString(locationId)}");

            if (!string.IsNullOrEmpty(locationFromCache))
            {
                location = JsonConvert.DeserializeObject<Location>(locationFromCache);

                locationDTO = _mapper.Map<LocationDTO>(location);
                return new GenericResponse<LocationDTO>
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = locationDTO
                };
            }

            location = await _locationQueryRepository.GetByDefaultAsync(x => x.ID == locationId);

            if (location != null)
            {
                await _cache.SetValueAsync(Convert.ToString(locationId), JsonConvert.SerializeObject(location));
                locationDTO = _mapper.Map<LocationDTO>(location);

                return new GenericResponse<LocationDTO>
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = locationDTO
                };
            }
            return new GenericResponse<LocationDTO>
            {
                Message = ResponseMessages.NoLocationRecordFound
            };
        }

        public async Task<LocationResponseModel> GetLocations()
        {
            List<Location> locations = new();
            List<LocationDTO> locationsDTO = new();

            string locationsFromCache = await _cache.GetValueAsync($"{LocationsCacheConstant}");

            if (!string.IsNullOrEmpty(locationsFromCache))
            {
                locations = JsonConvert.DeserializeObject<List<Location>>(locationsFromCache);

                locationsDTO = _mapper.Map<List<LocationDTO>>(locations);
                return new LocationResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = locationsDTO
                };
            }

            locations = (await _locationQueryRepository.GetAllAsync()).ToList();

            if (locations.Any())
            {
                locationsDTO = _mapper.Map<List<LocationDTO>>(locations);

                return new LocationResponseModel
                {
                    Status = true,
                    Message = ResponseMessages.Success,
                    Data = locationsDTO
                };
            }
            return new LocationResponseModel
            {
                Message = ResponseMessages.NoLocationRecordFound
            };
        }
    }
}
