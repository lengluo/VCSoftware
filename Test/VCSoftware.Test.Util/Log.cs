using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Reflection;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;

namespace VCSoftware.Test.Util
{
    public class Log : IClassFixture<LogFixture>
    {
        private readonly LogFixture _logFixture;

        public Log(LogFixture logFixture)
        {
            _logFixture = logFixture;
        }

        /// <summary>
        /// 跟踪
        /// </summary>
        [Fact]
        public void Trace()
        {
            VCUtil.Logger.Trace("woca");
        }

        /// <summary>
        /// 调试
        /// </summary>
        [Fact]
        public void Debug()
        {
            VCUtil.Logger.Debug("woca");
        }

        /// <summary>
        /// 一般信息
        /// </summary>
        [Fact]
        public void Info()
        {
            VCUtil.Logger.Info("woca");
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Fact]
        public void Warn()
        {
            VCUtil.Logger.Warn("woca");
        }

        /// <summary>
        /// 错误
        /// </summary>
        [Fact]
        public void Error()
        {
            VCUtil.Logger.Error("woca");
        }

        /// <summary>
        /// 严重错误
        /// </summary>
        [Fact]
        public void Fatal()
        {
            VCUtil.Logger.Fatal("woca");
        }
    }
}
