using JobBoard.Data.Repository;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace JobBoard.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));

        IUnitOfWorkTransaction BeginTransaction();
    }

    public interface IUnitOfWorkTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
