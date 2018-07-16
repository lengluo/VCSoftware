using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;
using System.Reflection;
using VCSoftware.Dao.DbProvider;
using VCSoftware.Dao.Repository;
using VCSoftware.Util;
using VCSoftware.Util.Log;
using Xunit;

namespace VCSoftware.Test.Dao
{
    public class Dao : IClassFixture<DaoFixture<User>>
    {
        private readonly DaoFixture<User> _daoFixture;

        public Dao(DaoFixture<User> daoFixture)
        {
            _daoFixture = daoFixture;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        [Fact]
        public void Query()
        {
            var data = _daoFixture.Repository.Query();
        }

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Insert()
        {
            var data = _daoFixture.Repository.Insert(new User
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
            var data = _daoFixture.Repository.Update(new User
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
            var data = _daoFixture.Repository.Delete("6e7f1b39-6f16-4af9-a6e0-0b9ea5bc594d");
        }

        /// <summary>
        /// ִ��
        /// </summary>
        [Fact]
        public void Execute()
        {
            var data = _daoFixture.Repository.Execute("select * from sys_user");
        }

        /// <summary>
        /// ִ�д洢���̲����ؽ��
        /// </summary>
        [Fact]
        public void ExecuteProcedure()
        {
            var para = new OracleDynamicParameters();
            para.Add("sTime", "0001-01-01");
            para.Add("eTime", "9999-12-31");
            para.Add("filterType", "�������");
            para.Add("officeIdsAcl", "1459839968518011");
            para.Add("out_cursor", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            var data = _daoFixture.Repository.ExecuteProcedure("pe_decisiontotalall", para);
        }
    }
}
