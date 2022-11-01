using Microsoft.Extensions.Caching.Distributed;
using MobileBanking.ServiceProviders.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileBanking.ServiceProviders
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly string APP_NAME = "MOBILE_BANKING";

        public CachingService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Set(string key, object value, int timeInSeconds = 60)
        {
            return Task.Run(() =>
            {
                try
                {
                    _distributedCache.SetString(key + $"_{APP_NAME}", JsonConvert.SerializeObject(value), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(timeInSeconds))
                    });
                }
                catch (Exception exp)
                {

                }
            });
        }

        public Task<T> Get<T>(string key)
        {
            return Task.Run(() =>
            {
                try
                {
                    var value = _distributedCache.GetString(key + $"_{APP_NAME}");
                    if (!string.IsNullOrEmpty(value))
                        return JsonConvert.DeserializeObject<T>(value);
                    else
                        return default;
                }
                catch (Exception exp)
                {
                    return default;
                }
            });
        }
    }
}
