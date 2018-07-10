using System;
using System.Collections.Generic;
using System.Text;
using VCSoftware.Dao.DbProvider;
using VCSoftware.Util;

namespace VCSoftware.Dao.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private IDbProvider _dbProvider;

        public Repository(IDbProvider dbProvider)
        {
            this._dbProvider = dbProvider;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public IEnumerable<T> Query(string queryParam = "", string fieldNames = "")
        {
            using (var conn = _dbProvider.GetConnection())
            {
                conn.Open();
                IEnumerable<T> result;
                result = _dbProvider.Query<T>(conn, queryParam, fieldNames);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Insert(T t)
        {
            using (var conn = _dbProvider.GetConnection())
            {
                conn.Open();
                var trans = _dbProvider.GetTransaction(conn);
                try
                {
                    int result;
                    result = _dbProvider.Insert<T>(conn, t);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    VCUtil.Logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Update(T t)
        {
            using (var conn = _dbProvider.GetConnection())
            {
                conn.Open();
                var trans = _dbProvider.GetTransaction(conn);
                try
                {
                    int result;
                    result = _dbProvider.Update<T>(conn, t);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    VCUtil.Logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            using (var conn = _dbProvider.GetConnection())
            {
                conn.Open();
                var trans = _dbProvider.GetTransaction(conn);
                try
                {
                    int result;
                    result = _dbProvider.Delete<T>(conn, id);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    VCUtil.Logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public int Execute(string sqlStr)
        {
            using (var conn = _dbProvider.GetConnection())
            {
                conn.Open();
                var trans = _dbProvider.GetTransaction(conn);
                try
                {
                    int result;
                    result = _dbProvider.Execute(conn, sqlStr);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    VCUtil.Logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
