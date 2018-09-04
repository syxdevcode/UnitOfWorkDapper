using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Data;
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
            IDbConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
    }
}
