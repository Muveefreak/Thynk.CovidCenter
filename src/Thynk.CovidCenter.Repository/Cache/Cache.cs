using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Threading.Tasks;
using Thynk.CovidCenter.Repository.Exceptions;

namespace Thynk.CovidCenter.Repository.Cache
{
    public class Cache : ICache
    {
        private readonly IDistributedCache _distributedCache;

        public Cache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public string GetValue(string key)
        {
            try
            {
                return _distributedCache.GetString(key);
            }
            catch (Exception ex)
            {

                throw new CachingException(ex.Message);
            }
        }

        public async Task<string> GetValueAsync(string key)
        {
            try
            {
                return await _distributedCache.GetStringAsync(key);
            }
            catch (Exception ex)
            {

                throw new CachingException(ex.Message);
            }
        }

        public void SetValue(string key, string value)
        {
            _distributedCache.SetString(key, value);
        }
        public async Task SetValueAsync(string key, string value)
        {
            try
            {
                await _distributedCache.SetStringAsync(key, value);
            }
            catch (Exception ex)
            {
                throw new CachingException(ex.Message);
            }
        }

        public void SetValue(string key, string value, int ttl)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(TimeSpan.FromMinutes(ttl));
            _distributedCache.SetString(key, value, options);
        }
        public async Task SetValueAsync(string key, string value, int ttl)
        {
            try
            {
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(TimeSpan.FromMinutes(ttl));
                await _distributedCache.SetStringAsync(key, value, options);
            }
            catch (Exception ex)
            {
                throw new CachingException(ex.Message);
            }
        }

        public async Task<bool> RemoveKeyAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
            return true;
        }

        public bool RemoveKey(string key)
        {
            _distributedCache.Remove(key);
            return true;
        }
    }
}
