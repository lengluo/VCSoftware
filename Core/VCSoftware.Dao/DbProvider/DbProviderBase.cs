using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VCSoftware.Util;

namespace VCSoftware.Dao.DbProvider
{
    public abstract class DbProviderBase : IDbProvider
    {
        /// <summary>
        /// 获取数据库链接
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection GetConnection()
        {
            throw new Exception("Please specify one of dbproviders!");
        }

        public virtual IDbTransaction GetTransaction(IDbConnection conn)
        {
            throw new Exception("Please specify one of dbproviders!");
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParam">查询条件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(IDbConnection conn, string queryParam = "", string fieldNames = "") where T : BaseEntity
        {
            //获取实体信息
            var entityInfo = new EntityMapping<T>();
            var dataTableName = entityInfo.GetTableName();
            var sqlStr = $"select * from {dataTableName}";
            if (!string.IsNullOrEmpty(queryParam.Trim()))
                sqlStr += $" where {queryParam}";
            var data = SqlMapper.Query<T>(conn, sqlStr);
            return data;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public virtual int Insert<T>(IDbConnection conn, T t) where T : BaseEntity
        {
            //获取实体信息
            var entityInfo = new EntityMapping<T>();
            var dataTableName = entityInfo.GetTableName();
            //获取实体字段
            var fields = entityInfo.GetFields(t).Where(l => l.Value != null && l.Value.ToString() != string.Empty);
            var strFieldNames = string.Join(',', fields.Select(l => l.Name));
            var strFieldVals = string.Join(',', fields.Select(l => $"'{l.Value}'"));
            var sqlStr = $"insert into {dataTableName}({strFieldNames}) values ({strFieldVals})";
            var data = SqlMapper.Execute(conn, sqlStr);
            return data;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public virtual int Update<T>(IDbConnection conn, T t) where T : BaseEntity
        {
            //获取实体信息
            var entityInfo = new EntityMapping<T>();
            var dataTableName = entityInfo.GetTableName();
            //获取实体字段
            var fields = entityInfo.GetFields(t).Where(l => l.Value != null && l.Value.ToString() != string.Empty);
            var strFieldNames = string.Join(',', fields.Where(l => !l.IsKey).Select(l => l.Name));
            var strFieldVals = string.Join(',', fields.Where(l => !l.IsKey).Select(l => $"'{l.Value}'"));
            //获取主键key
            var keyField = fields.FirstOrDefault(l => l.IsKey);
            if (keyField == null) throw new Exception("key was not found!");
            var paramStr = string.Join(',', fields.Select(l => string.Format("{0}={1}", l.Name, l.Value is String ? $"'{l.Value}'" : l.Value)));
            var sqlStr = $"update {dataTableName} set {paramStr} where {keyField.Name}= '{keyField.Value}'";
            var data = SqlMapper.Execute(conn, sqlStr);
            return data;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="id">当前主键的值</param>
        /// <returns></returns>
        public virtual int Delete<T>(IDbConnection conn, string id) where T : BaseEntity
        {
            //获取实体信息
            var entityInfo = new EntityMapping<T>();
            var dataTableName = entityInfo.GetTableName();
            //获取主键key
            var keyFieldName = entityInfo.GetKeyFieldName();
            if (keyFieldName == null) throw new Exception("key was not found!");
            var sqlStr = $"delete from {dataTableName} where {keyFieldName}= '{id}'";
            var data = SqlMapper.Execute(conn, sqlStr);
            return data;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public virtual int Execute(IDbConnection conn, string sqlStr)
        {
            var data = SqlMapper.Execute(conn, sqlStr);
            return data;
        }

        /// <summary>
        /// 执行存储过程并返回结果
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="param">存储过程参数</param>
        /// <returns></returns>
        public virtual DataTable ExecuteProcedure(IDbConnection conn, string procedureName, Dapper.SqlMapper.IDynamicParameters param)
        {
            var dt = new DataTable();
            var gridReader = SqlMapper.ExecuteReader(conn, procedureName, param, commandType: CommandType.StoredProcedure);
            dt.Load(gridReader);
            return dt;
        }
    }
}
