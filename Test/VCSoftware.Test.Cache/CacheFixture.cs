using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;

namespace VCSoftware.Test.Cache
{
    public class CacheFixture : IDisposable
    {
        public VCSoftware.Cache.Cache.Cache Cache { get; private set; }


        public CacheFixture()
        {
            Cache = new VCSoftware.Cache.Cache.Cache();
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
        }

        public void Dispose()
        {

        }
    }
}
