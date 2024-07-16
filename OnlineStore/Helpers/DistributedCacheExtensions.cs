using Microsoft.Extensions.Caching.Distributed;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisDemo.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static Task SetRecordAsync<CartItem>(this IDistributedCache cache,
            string recordId,
            IList<CartItem> data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromDays(30);
            options.SlidingExpiration = unusedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);
            var res =  cache.SetStringAsync(recordId, jsonData, options);
            return Task.FromResult(res); 
        }

        public static async Task<IEnumerable<CartItem>?> GetRecordAsync(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<IEnumerable<CartItem>>(jsonData);
        }

        public static async Task DeleteRecordAsync(this IDistributedCache cache, string cartId) =>
            await cache.RemoveAsync(cartId);
    }
}
