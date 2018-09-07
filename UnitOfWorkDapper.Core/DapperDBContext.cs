using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace UnitOfWorkDapper.Core
{
    /// <summary>
    /// Represents a context to Dapper.NET
    /// </summary>
    public abstract class DapperDBContext : IContext
    {
        #region Fields

        /// <summary>
        /// The connection object to database.
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// The transaction object to connection.
        /// </summary>
        private IDbTransaction _transaction;

        /// <summary>
        /// The command timeout.
        /// </summary>
        private int? _commandTimeout = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Indicates if transaction is started.
        /// </summary>
        public bool IsTransactionStarted { get; private set; }

        #endregion Properties

        private readonly DapperDBContextOptions _options;

        /// <summary>
        /// Creates connection object to database.
        /// </summary>
        /// <returns>The connection object.</returns>
        protected abstract IDbConnection CreateConnection(string connectionString);

        protected DapperDBContext(IOptions<DapperDBContextOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;

            _connection = CreateConnection(_options.Configuration);

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            DebugPrint("Connection started.");
        }

        #region Transaction

        public void BeginTransaction()
        {
            if (IsTransactionStarted)
                throw new InvalidOperationException("Transaction is already started.");

            _transaction = _connection.BeginTransaction();
            IsTransactionStarted = true;

            DebugPrint("Transaction started.");
        }

        public void Commit()
        {
            if (!IsTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _transaction.Commit();
            _transaction = null;

            IsTransactionStarted = false;

            DebugPrint("Transaction committed.");
        }

        public void Rollback()
        {
            if (!IsTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;

            IsTransactionStarted = false;

            DebugPrint("Transaction rollbacked and disposed.");
        }

        #endregion Transaction

        #region Dapper Execute & Query

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await _connection.ExecuteAsync(sql, param, _transaction, _commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await _connection.QueryAsync<T>(sql, param, _transaction, _commandTimeout, commandType);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, _transaction, _commandTimeout, commandType);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
        {
            return await _connection.QueryAsync(sql, map, param, _transaction, true, splitOn, _commandTimeout, commandType);
        }

        #endregion Dapper Execute & Query

        public void Dispose()
        {
            if (IsTransactionStarted)
                Rollback();

            _connection.Close();
            _connection.Dispose();
            _connection = null;

            DebugPrint("Connection closed and disposed.");
        }

        private void DebugPrint(string message)
        {
#if DEBUG
            Debug.Print(">>> UnitOfWorkWithDapper - Thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, message);
#endif
        }
    }
}