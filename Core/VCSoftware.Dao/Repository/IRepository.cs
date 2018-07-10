using System;
using System.Collections.Generic;
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
    }
}
