using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VCSoftware.Cache.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存value</param>
        /// <returns></returns>
        void Add(string key, object value, TimeSpan expiredTime = default(TimeSpan), ExpiredTimeType expiredTimeType = ExpiredTimeType.None, IChangeToken changeToken = null);

        /// <summary>
        /// 插入并更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void Set(string key, object value, TimeSpan expiredTime = default(TimeSpan), ExpiredTimeType expiredTimeType = ExpiredTimeType.None, IChangeToken changeToken = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

    }
}
