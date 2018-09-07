using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkDapper.Core.Helper
{
    public class DapperHelper<T> where T : class, IDbConnection, new()
    {
        private IDbConnection conn;

        private string conStr = string.Empty;

        public DapperHelper(string connectionString)
        {
            conStr = connectionString;
            conn = new T();
            conn.ConnectionString = connectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, object parameters = null)
        {
            return conn.Execute(sql, parameters);
        }

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(string sql, object parameters = null)
        {
            return await conn.ExecuteAsync(sql, parameters);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, object parameters = null)
        {
            return conn.ExecuteScalar(sql, parameters);
        }

        /// <summary>
        /// 单个数据集查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="where"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<TEntity> Query<TEntity>(string sql, Func<TEntity, bool> where, object parameters = null)
        {
            return conn.Query<TEntity>(sql, parameters, commandTimeout: 0).Where(where).ToList();
        }

        /// <summary>
        /// 单个数据集查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<TEntity> Query<TEntity>(string sql, object parameters = null)
        {
            return conn.Query<TEntity>(sql, parameters, commandTimeout: 0).ToList();
        }

        /// <summary>
        /// 多个数据集查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">DynamicParameters</param>
        /// <returns></returns>
        public SqlMapper.GridReader MultyQuery(string sql, object parameters = null)
        {
            return conn.QueryMultiple(sql, parameters, commandTimeout: 0);
        }

        public IEnumerable<Entity> Query<Entity>(Func<Entity> typeBuilder, string sql, object parameters = null)
        {
            return conn.Query<Entity>(sql, parameters, commandTimeout: 0);
        }

        public TResult Execute<TResult>(Func<T, IDbTransaction, TResult> action, bool useTran)
        {
            using (var conn = new T())
            {
                conn.ConnectionString = this.conStr;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                if (useTran)
                {
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            TResult result = action(conn, tran);
                            tran.Commit();
                            return result;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            //Logger.Log("数据库操作异常", ex);
                            return default(TResult);
                        }
                    }
                }
                else
                {
                    return action(conn, null);
                }
            }
        }

        /// <summary>
        ///  执行--使用事物
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="action"></param>
        /// <param name="useTran"></param>
        public void Execute(string connectionString, Action<T, IDbTransaction> action, bool useTran)
        {
            using (var conn = new T())
            {
                conn.ConnectionString = connectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                if (useTran)
                {
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            action(conn, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    action(conn, null);
                }
            }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <typeparam name="T">查询实体</typeparam>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="pageCriteria">查询条件</param>
        /// <param name="totalNum">总数</param>
        /// <returns></returns>
        public PageDataView<T> GetPageListForSQL<T>(PageCriteria pageCriteria)
        {
            var result = new PageDataView<T>();
            string sql = "SELECT * from(SELECT " + pageCriteria.Fields + ",row_number() over(order by " + pageCriteria.Sort + ") rownum FROM " + pageCriteria.TableName + " where " + pageCriteria.Condition + ") t where rownum>@minrownum and rownum<=@maxrownum";
            string countSql = "select count(1) from " + pageCriteria.TableName + "  where " + pageCriteria.Condition;
            int minrownum = (pageCriteria.CurrentPage - 1) * pageCriteria.PageSize;
            int maxrownum = minrownum + pageCriteria.PageSize;
            var p = new DynamicParameters();
            p.Add("minrownum", minrownum);
            p.Add("maxrownum", maxrownum);
            if (pageCriteria.ParameterList != null)
            {
                foreach (var param in pageCriteria.ParameterList)
                {
                    p.Add(param.ParamName, param.ParamValue);
                }
            }
            var reader = MultyQuery(sql + ";" + countSql, p);
            result.Items = reader.Read<T>().ToList();
            result.TotalNum = reader.Read<int>().First<int>();
            result.CurrentPage = pageCriteria.CurrentPage;
            result.TotalPageCount = result.TotalNum / pageCriteria.PageSize + (result.TotalNum % pageCriteria.PageSize == 0 ? 0 : 1);
            return result;
        }
    }

    /// <summary>
    /// 分页查询获取数据实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDataView<T>
    {
        private int _TotalNum;

        public PageDataView()
        {
            this._Items = new List<T>();
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalNum
        {
            get { return _TotalNum; }
            set { _TotalNum = value; }
        }

        private IList<T> _Items;

        /// <summary>
        /// 具体数据列表
        /// </summary>
        public IList<T> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
    }

    /// <summary>
    /// 分页实体
    /// </summary>
    public class PageCriteria
    {
        public PageCriteria()
        {
            ParameterList = new List<ParameterDict>();
        }

        /// <summary>
        /// 查询的表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 主键名称
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 传入的参数列表
        /// </summary>
        public IList<ParameterDict> ParameterList { get; set; }
    }

    /// <summary>
    /// 参数字典
    /// </summary>
    public class ParameterDict
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object ParamValue { get; set; }
    }
}