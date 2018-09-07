using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkDapper.Core.Helper
{
    public class DapperHelper<T> where T : class, IDbConnection, new()
    {
        private static IDbConnection CreateConnection(string connectionString)
        {
            var conn = new T();
            conn.ConnectionString = connectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            return conn;
        }

        private static async Task<IDbConnection> CrateConnectionAsync(string connectionString)
        {
            var conn = new T();
            conn.ConnectionString = connectionString;
            var tempconn = conn as SqlConnection;
            if (conn.State != ConnectionState.Open)
            {
                if (tempconn != null)
                {
                    await tempconn.OpenAsync();
                }
            }

            return tempconn;
        }

        /// <summary>
        /// 执行增、删、改方法
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, string sql, object parameters = null)
        {
            using (IDbConnection conn = CreateConnection(connectionString))
            {
                return conn.Execute(sql, parameters);
            }
        }

        public static Task<int> ExecuteNonQueryAsync(string connectionString, string sql, object parameters = null)
        {
            using (IDbConnection conn = CrateConnectionAsync(connectionString).Result)
            {
                return conn.ExecuteAsync(sql, parameters);
            }
        }

        /// <summary>
        /// 得到单行单列
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, string sql, object parameters = null)
        {
            using (IDbConnection conn = CreateConnection(connectionString))
            {
                return conn.ExecuteScalar(sql, parameters);
            }
        }

        /// <summary>
        /// 单个数据集查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="where"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<TEntity> Query<TEntity>(string connectionString, string sql, Func<TEntity, bool> where, object parameters = null)
        {
            using (IDbConnection conn = CreateConnection(connectionString))
            {
                return conn.Query<TEntity>(sql, parameters, commandTimeout: 0).Where(where).ToList();
            }
        }

        /// <summary>
        /// 单个数据集查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<TEntity> Query<TEntity>(string connectionString, string sql, object parameters = null)
        {
            using (IDbConnection conn = CreateConnection(connectionString))
            {
                return conn.Query<TEntity>(sql, parameters, commandTimeout: 0).ToList();
            }
        }

        /// <summary>
        /// 多个数据集查询
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters">DynamicParameters</param>
        /// <returns></returns>
        public static SqlMapper.GridReader MultyQuery(string connectionString, string sql, object parameters = null)
        {
            IDbConnection conn = CreateConnection(connectionString);
            return conn.QueryMultiple(sql, parameters, commandTimeout: 0);
        }

        /// <summary>
        /// demo： var data = connection.Query(() => new {Id = default(int),Name = default(string),}, "SELECT Id, Name FROM Table");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="typeBuilder"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<Entity> Query<Entity>(string connectionString, Func<Entity> typeBuilder, string sql, object parameters = null)
        {
            using (IDbConnection conn = CreateConnection(connectionString))
            {
                return conn.Query<Entity>(sql, parameters, commandTimeout: 0);
            }
        }

        /// <summary>
        /// demo：
        /// DatabaseHelper<SQLiteConnection>.Execute(connectionStr, x =>{return x.Query<int>("select 1");}, true);
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="action"></param>
        /// <param name="useTran"></param>
        /// <returns></returns>
        public static TResult Execute<TResult>(string connectionString, Func<T, IDbTransaction, TResult> action, bool useTran)
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
        /// DatabaseHelper<SQLiteConnection>.Execute(connectionStr, x =>{x.Query<int>("delete from Test where Id=1");x.Query<int>("select * from Test where Id=1");}, true);
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="action"></param>
        /// <param name="useTran"></param>
        public static void Execute(string connectionString, Action<T, IDbTransaction> action, bool useTran)
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
                            //Logger.Log("数据库操作异常", ex);
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
        public static PageDataView<T> GetPageListForSQL<T>(string connectionString, PageCriteria pageCriteria)
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
            var reader = MultyQuery(connectionString, sql + ";" + countSql, p);
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