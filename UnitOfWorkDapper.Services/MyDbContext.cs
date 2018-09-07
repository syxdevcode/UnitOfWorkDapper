using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using UnitOfWorkDapper.Core;

namespace UnitOfWorkDapper.Services
{
    public class MyDbContext : DapperDBContext
    {
        public MyDbContext(IOptions<DapperDBContextOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        /// <summary>
        /// Creates connection object to database. This method is called in creating the instance of context.
        /// </summary>
        /// <returns>The connection object.</returns>
        protected override IDbConnection CreateConnection(string connectionString)
        {
            // mysql
            //IDbConnection conn = new MySqlConnection(connectionString);

            // sql
            IDbConnection conn = new SqlConnection(connectionString);

            return conn;
        }
    }
}