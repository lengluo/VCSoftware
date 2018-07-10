using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VCSoftware.Util;

namespace VCSoftware.Dao.DbProvider
{
    public class OracleDbProvider : DbProviderBase, IDbProvider
    {
        /// <summary>
        /// 获取数据库链接
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetConnection()
        {
            var connString = VCUtil.Config.Configuration["ConnectionStrings:DefaultConnection"];
            var conn = new OracleConnection(connString);
            return conn;
        }

        /// <summary>
        /// 获取事务
        /// </summary>
        /// <returns></returns>
        public override IDbTransaction GetTransaction(IDbConnection conn)
        {
            var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            return trans;
        }
    }
}
