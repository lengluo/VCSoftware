using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VCSoftware.Dao.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        IEnumerable<T> Query(string queryParam = "", string fieldNames = "");

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert(T t);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Update(T t);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(string id);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        int Execute(string sqlStr);

        /// <summary>
        /// 执行存储过程并返回结果
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="param">存储过程参数</param>
        /// <returns></returns>
        DataTable ExecuteProcedure(string procedureName, Dapper.SqlMapper.IDynamicParameters param);
    }
}
