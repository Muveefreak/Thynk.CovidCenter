using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.Constants;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Data.Models;
using Thynk.CovidCenter.Repository.Cache;
using Thynk.CovidCenter.Repository.Queries.Interfaces;

namespace Thynk.CovidCenter.Core.Helpers
{
    public class Utilities : IUtilities
    {
        private readonly IDBQueryRepository<ApplicationUser> _applicationUserQueryRepository;
        private readonly IDBQueryRepository<Location> _locationQueryRepository;
        private readonly ICache _cache;

        public Utilities(IDBQueryRepository<ApplicationUser> applicationUserQueryRepository,
            IDBQueryRepository<Location> locationQueryRepository,
            ICache cache)
        {
            _applicationUserQueryRepository = applicationUserQueryRepository;
            _locationQueryRepository = locationQueryRepository;
            _cache = cache;
        }
        public async Task<ApplicationUser> GetUser(Guid userId)
        {
            string userFromCache = await _cache.GetValueAsync($"{CacheConstants.UserCacheConstant}_{userId}");

            if (!string.IsNullOrEmpty(userFromCache))
            {
                return JsonConvert.DeserializeObject<ApplicationUser>(userFromCache);
            }
            return await _applicationUserQueryRepository.GetByDefaultAsync(x => x.ID == userId);
        }

        public async Task<Location> GetLocation(Guid locationId)
        {
            string locationFromCache = await _cache.GetValueAsync($"{CacheConstants.LocationsCacheConstant}_{locationId}");

            if (!string.IsNullOrEmpty(locationFromCache))
            {
                return JsonConvert.DeserializeObject<Location>(locationFromCache);
            }
            return await _locationQueryRepository.GetByDefaultAsync(x => x.ID == locationId);
        }
    }
}
