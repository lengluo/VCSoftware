using System;
using System.IO;
using System.Reflection;
using VCSoftware.Util;
using VCSoftware.Util.Log;

namespace VCSoftware.Test.Util
{
    public class LogFixture: IDisposable 
    {
      
        public LogFixture()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
