using Application.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        //private readonly IDistributedCache distributedCache;
        private readonly IdempotencyCacheOptions cacheOptions;

        public RequestManager(
            // IDistributedCache distributedCache,
            IOptionsMonitor<IdempotencyCacheOptions> cacheOptions)
        {
            //this.distributedCache = distributedCache;
            this.cacheOptions = cacheOptions.CurrentValue;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            string key = GetKey<T>(id);
            /*
            await distributedCache.SetStringAsync(key,
                $"Command: {typeof(T)} - Id: {id} Time: {DateTime.Now}",
                new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(cacheOptions.IdempotencyCacheTimeMinutes) }
            );
            */
        }

        private static string GetKey<T>(Guid id)
        {
            var name = typeof(T).Name;
            return $"{name}:{id}";
        }

        public async Task<bool> ExistAsync<T>(Guid id)
        {
            /*string key = GetKey<T>(id);
            string jsonResult = await distributedCache.GetStringAsync(key);
            if (!string.IsNullOrWhiteSpace(jsonResult))
                return true;
            */

            return false;
        }
    }
}
