using System;
using System.IO;
using System.Reflection;
using VCSoftware.Dao.DbProvider;
using VCSoftware.Dao.Repository;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;

namespace VCSoftware.Test.Util
{
    public class Dao
    {
        /// <summary>
        /// ��ѯ
        /// </summary>
        [Fact]
        public void Query()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();

            var repo = new Repository<User>(new OracleDbProvider());
            var data = repo.Query();
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Insert()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();

            VCUtil.Config.InitConfig(webroot, "appsettings.json");

            var repo = new Repository<User>(new OracleDbProvider());
            var data = repo.Insert(new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "core"
            });
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Update()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();

            VCUtil.Config.InitConfig(webroot, "appsettings.json");

            var repo = new Repository<User>(new OracleDbProvider());
            var data = repo.Update(new User
            {
                Id = "b7d95362-a2c9-4322-8f50-65b46016d1e6",
                Name = "core22"
            });
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        [Fact]
        public void Delete()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();

            VCUtil.Config.InitConfig(webroot, "appsettings.json");

            var repo = new Repository<User>(new OracleDbProvider());
            var data = repo.Delete("6e7f1b39-6f16-4af9-a6e0-0b9ea5bc594d");
        }

        /// <summary>
        /// ִ��
        /// </summary>
        [Fact]
        public void Execute()
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var webroot = pathToDllDirectory.ToString().Substring(0, pathToDllDirectory.ToString().IndexOf("bin"));

            VCUtil.Config.InitConfig(webroot, "appsettings.json");
            VCUtil.Logger.loggerMgr = new Log4NetLoggerManager();

            var repo = new Repository<User>(new OracleDbProvider());
            var data = repo.Execute("select * from sys_user");
        }
    }
}
