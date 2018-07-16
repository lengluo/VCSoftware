using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;
using Xunit.Abstractions;

namespace VCSoftware.Test.Cache
{
    public class Cache : IClassFixture<CacheFixture>
    {
        private CacheFixture _cacheFixture;
        private ITestOutputHelper _output;

        public Cache(CacheFixture cacheFixture, ITestOutputHelper output)
        {
            _cacheFixture = cacheFixture;
            _output = output;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        [Fact]
        public void Add()
        {
            _cacheFixture.Cache.Add("vitchen", "well");
            var result = _cacheFixture.Cache.Get("vitchen");
            _output.WriteLine(result == null ? "" : result.ToString());
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        [Fact]
        public void Get()
        {
            _cacheFixture.Cache.Add("vitchen", "well");
            var result = _cacheFixture.Cache.Get("vitchen");
            _output.WriteLine(result == null ? "" : result.ToString());
        }

        /// <summary>
        /// 添加缓存，绝对过期
        /// </summary>
        [Fact]
        public void AddAbExpire()
        {
            _cacheFixture.Cache.Add("vitchen", "well", TimeSpan.FromSeconds(2), VCSoftware.Cache.ExpiredTimeType.Absolute);
            var result = _cacheFixture.Cache.Get("vitchen");
            Thread.Sleep(3000);
            var result2 = _cacheFixture.Cache.Get("vitchen");
            _output.WriteLine(result == null ? "" : result.ToString());
            _output.WriteLine(result2 == null ? "" : result2.ToString());
        }

        /// <summary>
        /// 添加缓存，相对过期
        /// </summary>
        [Fact]
        public void AddRlExpire()
        {
            _cacheFixture.Cache.Add("vitchen", "well", TimeSpan.FromSeconds(2), VCSoftware.Cache.ExpiredTimeType.Sliding);
            var result = _cacheFixture.Cache.Get("vitchen");
            _cacheFixture.Cache.Set("vitchen", "wella");
            var result2 = _cacheFixture.Cache.Get("vitchen");
            Thread.Sleep(3000);
            var result3 = _cacheFixture.Cache.Get("vitchen");

            _output.WriteLine(result == null ? "" : result.ToString());
            _output.WriteLine(result2 == null ? "" : result2.ToString());
            _output.WriteLine(result3 == null ? "" : result3.ToString());
        }

        /// <summary>
        /// 添加缓存，自定义过期
        /// </summary>
        [Fact]
        public void AddCustomExpire()
        {
            var ck = new ChangeToken();
            _cacheFixture.Cache.Set("vitchen", "well", TimeSpan.FromSeconds(0), VCSoftware.Cache.ExpiredTimeType.Custom, ck);
            var result = _cacheFixture.Cache.Get("vitchen");
            Thread.Sleep(1000);
            _cacheFixture.Cache.Set("vitchen", "wella", TimeSpan.FromSeconds(0), VCSoftware.Cache.ExpiredTimeType.Custom, ck);
            var result2 = _cacheFixture.Cache.Get("vitchen");

            var ck2 = new ChangeTokenCallback();
            int cnt = 0;
            _cacheFixture.Cache.Set("vitchen1", "wellb", TimeSpan.FromSeconds(0), VCSoftware.Cache.ExpiredTimeType.Custom, ck2);
            var result3 = _cacheFixture.Cache.Get("vitchen1");
            ck2.RegisterChangeCallback(p => cnt++, 1);
            _output.WriteLine(result == null ? "" : result.ToString());
            _output.WriteLine(result2 == null ? "" : result2.ToString());
            _output.WriteLine(result3 == null ? "" : result3.ToString());
        }
    }
}
