using System;

namespace UnitOfWorkDapper.Core
{
     /// <summary>
    /// The interface of unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save changes into context.
        /// </summary>
        bool SaveChanges();
    }
}
