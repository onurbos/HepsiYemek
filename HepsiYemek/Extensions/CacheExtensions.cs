using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace HepsiYemek.Extensions
{
    public static class CacheExtensions
    {
        public static async Task SetRecordAsync<T>(IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? unUsedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = unUsedExpireTime
            };

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T> GetRecordAsync<T>(IDistributedCache cache, string recordId)
        {

            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                //await SetRecordAsync();
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public static async Task Remove(IDistributedCache cache, string recordId)
        {
            await cache.RemoveAsync(recordId);
        }
    }
}
