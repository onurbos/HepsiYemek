using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace HepsiYemek.Extensions
{
    public interface ICacheExtensions
    {
        Task SetRecordAsync<T>(IDistributedCache cache, string recordId, T data, TimeSpan? unUsedExpireTime = null);
        Task<BsonDocument> GetRecordAsync(IDistributedCache cache, string recordId);
        Task Remove(IDistributedCache cache, string recordId);
    }


    public abstract class CacheExtensions : ICacheExtensions
    {
        public async Task SetRecordAsync<T>(IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? unUsedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = unUsedExpireTime
            };

            await cache.SetStringAsync(recordId, data.ToString(), options);
        }

        public async Task<BsonDocument> GetRecordAsync(IDistributedCache cache, string recordId)
        {
            var data = await cache.GetStringAsync(recordId);

            return data is null ? null : BsonDocument.Parse(data);
        }

        public async Task Remove(IDistributedCache cache, string recordId)
        {
            var result = await cache.GetAsync(recordId);
            if (result == null) return;
            await cache.RemoveAsync(recordId);
        }
    }
}