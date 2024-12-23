using Microsoft.Extensions.Caching.Memory;
using SympliSearch.Application.Interfaces;

namespace SympliSearch.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Set<T>(string key, T value, TimeSpan duration)
        {
            _memoryCache.Set(key, value, duration);
        }

        public bool TryGet<T>(string key, out T? value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }
    }
}
