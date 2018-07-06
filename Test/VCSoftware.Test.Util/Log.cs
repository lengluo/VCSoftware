using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Reflection;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;

namespace VCSoftware.Test.Util
{
    public class Log
    {
        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Trace()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot,"appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
            VCUtil.Logger.Trace("woca");
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Debug()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
            VCUtil.Logger.Debug("woca");
        }

        /// <summary>
        /// һ����Ϣ
        /// </summary>
        [Fact]
        public void Info()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
            VCUtil.Logger.Info("woca");
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Warn()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
            VCUtil.Logger.Warn("woca");
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Error()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
            VCUtil.Logger.Error("woca");
        }

        /// <summary>
        /// ���ش���
        /// </summary>
        [Fact]
        public void Fatal()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();
            VCUtil.Logger.Fatal("woca");
        }
    }
}
