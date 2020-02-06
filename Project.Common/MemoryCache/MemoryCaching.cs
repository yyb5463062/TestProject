using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.MemoryCache
{
    /// <summary>
    /// 实例化缓存接口ICaching
    /// </summary>
    public class MemoryCaching : ICaching
    {
        //引用Microsoft.Extensions.Caching.Memory;这个和.net 还是不一样，没有了Httpruntime了
        private readonly IMemoryCache _cache;
        //还是通过构造函数的方法，获取
        public MemoryCaching(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool Exists(string cacheKey)
        {
            return _cache.TryGetValue(cacheKey,out object obj);
        }

        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue)
        {
            _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(60*30));
        }
    }
}
