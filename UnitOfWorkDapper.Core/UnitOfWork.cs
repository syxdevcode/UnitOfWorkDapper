﻿using System;

namespace UnitOfWorkDapper.Core
{
    /// <summary>
    /// Represents an unit of work implementation.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The context object.
        /// </summary>
        private readonly IContext _context;

        /// <summary>
        /// Creates new unit of work instance, beginning a new transaction to the database.
        /// </summary>
        /// <param name="context">The context object.</param>
        public UnitOfWork(IContext context)
        {
            _context = context;

            // begins transaction
            _context.BeginTransaction();
        }

        /// <summary>
        /// Save changes into context.
        /// </summary>
        public bool SaveChanges()
        {
            if (!_context.IsTransactionStarted)
                throw new InvalidOperationException("Transaction have already been commited or disposed.");

            // commits transation
            _context.Commit();

            return true;
        }

        public void Dispose()
        {
            if (_context.IsTransactionStarted)
            {
                // rollback transaction
                _context.Rollback();
            }
        }
    }
}