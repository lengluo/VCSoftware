using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VCSoftware.Dao.DbProvider
{
    public interface IDbProvider
    {
        /// <summary>
        /// 获取数据库链接
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();

        /// <summary>
        /// 获取事务
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        IDbTransaction GetTransaction(IDbConnection conn);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(IDbConnection conn, string queryParam = "", string fieldNames = "") where T : BaseEntity;

        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        int Insert<T>(IDbConnection conn, T t) where T : BaseEntity;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        int Update<T>(IDbConnection conn, T t) where T : BaseEntity;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="id">当前主键的值</param>
        /// <returns></returns>
        int Delete<T>(IDbConnection conn, string id) where T : BaseEntity;

        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        int Execute(IDbConnection conn, string sqlStr);
    }
}
