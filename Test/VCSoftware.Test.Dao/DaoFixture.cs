using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using VCSoftware.Dao;
using VCSoftware.Dao.DbProvider;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;

namespace VCSoftware.Test.Dao
{
    public class DaoFixture<T> : IDisposable where T : BaseEntity
    {
        public VCSoftware.Dao.Repository.Repository<T> Repository
        {
            get; private set;
        }

        public DaoFixture()
        {
            Repository = new VCSoftware.Dao.Repository.Repository<T>(new OracleDbProvider());
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
