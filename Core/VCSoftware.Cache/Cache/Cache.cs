using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;

namespace VCSoftware.Cache.Cache
{
    public class Cache : ICache
    {

        private MemoryCache _memoryCache;

        public Cache()
        {
            _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.Set(key, value);
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public object Get(string key)
        {
            object result;
            _memoryCache.TryGetValue(key, out result);
            return result;
        }

        /// <summary>
        /// 添加，已存在则抛错
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存value</param>
        /// <param name="expiredTime">超期时间</param>
        /// <param name="expiredTimeType">超期类型</param>
        /// <param name="changeToken">超期自定义ChangeToken，可用以自定义条件和回调</param>
        /// <returns></returns>
        public void Add(string key, object value, TimeSpan expiredTime = default(TimeSpan), ExpiredTimeType expiredTimeType = ExpiredTimeType.None, IChangeToken changeToken = null)
        {
            object obj;
            var isExist = _memoryCache.TryGetValue(key, out obj);
            if (isExist) throw new Exception("Cache contains this key!");
            this.Set(key, value, expiredTime, expiredTimeType, changeToken);
        }

        /// <summary>
        /// 添加，已存在则更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiredTime">超期时间</param>
        /// <param name="expiredTimeType">超期类型</param>
        /// <param name="changeToken">超期自定义ChangeToken，可用以自定义条件和回调</param>
        /// <returns></returns>
        public void Set(string key, object value, TimeSpan expiredTime = default(TimeSpan), ExpiredTimeType expiredTimeType = ExpiredTimeType.Absolute, IChangeToken changeToken = null)
        {
            //设置默认超时时间
            if (expiredTime == default(TimeSpan))
                expiredTime = TimeSpan.FromMinutes(20);
            object obj;
            var isExist = _memoryCache.TryGetValue(key, out obj);
            if (!isExist)
            {
                _memoryCache.GetOrCreate(key, v =>//幻读？
                {
                    switch (expiredTimeType)
                    {
                        case ExpiredTimeType.Absolute:
                            v.SetAbsoluteExpiration(expiredTime);
                            break;
                        case ExpiredTimeType.Sliding:
                            v.SetSlidingExpiration(expiredTime);
                            break;
                        case ExpiredTimeType.Custom:
                            v.AddExpirationToken(changeToken);
                            break;
                        default:
                            break;
                    }
                    return value;
                });
            }
            else
            {
                switch (expiredTimeType)
                {
                    case ExpiredTimeType.Absolute:
                        _memoryCache.Set(key, value, DateTime.UtcNow.Add(expiredTime));
                        break;
                    case ExpiredTimeType.Sliding:
                        _memoryCache.Set(key, value, expiredTime);
                        break;
                    case ExpiredTimeType.Custom:
                        _memoryCache.Set(key, value, changeToken);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
