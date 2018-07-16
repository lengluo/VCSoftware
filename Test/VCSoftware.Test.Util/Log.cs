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
        /// ����
        /// </summary>
        [Fact]
        public void Trace()
        {
            VCUtil.Logger.Trace("woca");
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Debug()
        {
            VCUtil.Logger.Debug("woca");
        }

        /// <summary>
        /// һ����Ϣ
        /// </summary>
        [Fact]
        public void Info()
        {
            VCUtil.Logger.Info("woca");
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Warn()
        {
            VCUtil.Logger.Warn("woca");
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Error()
        {
            VCUtil.Logger.Error("woca");
        }

        /// <summary>
        /// ���ش���
        /// </summary>
        [Fact]
        public void Fatal()
        {
            VCUtil.Logger.Fatal("woca");
        }
    }
}
