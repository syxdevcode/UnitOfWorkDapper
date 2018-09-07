﻿using System;

namespace UnitOfWorkDapper.Core
{
    /// <summary>
    /// The interface of context.
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        /// Indicates if transaction is started.
        /// </summary>
        bool IsTransactionStarted { get; }

        /// <summary>
        /// Begins transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits operations of transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks operations of transaction.
        /// </summary>
        void Rollback();
    }
}