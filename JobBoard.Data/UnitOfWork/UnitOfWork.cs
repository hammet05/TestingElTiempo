using JobBoard.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;

namespace JobBoard.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;
        private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(AppDbContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));

            _ctx.Configuration.LazyLoadingEnabled = false;
            _ctx.Configuration.ProxyCreationEnabled = false;
            _ctx.Configuration.AutoDetectChangesEnabled = true;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var t = typeof(T);
            if (!_repositories.ContainsKey(t))
                _repositories[t] = new JobBoard.Data.Repository.Repository<T>(_ctx); 

            return (IRepository<T>)_repositories[t];
        }

        public int SaveChanges() => _ctx.SaveChanges();

        public Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken))
            => _ctx.SaveChangesAsync(ct);

        public IUnitOfWorkTransaction BeginTransaction() => new EfUnitOfWorkTransaction(_ctx);

        public void Dispose() => _ctx.Dispose();
    }

    internal sealed class EfUnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly DbContext _ctx;
        private readonly DbContextTransaction _tx;
        private bool _completed;

        public EfUnitOfWorkTransaction(DbContext ctx)
        {
            _ctx = ctx;
            _tx = _ctx.Database.BeginTransaction();
        }

        public void Commit()
        {
            _ctx.SaveChanges();
            _tx.Commit();
            _completed = true;
        }

        public void Rollback()
        {
            _tx.Rollback();
            _completed = true;
        }

        public void Dispose()
        {
            if (!_completed)
                _tx.Rollback();

            _tx.Dispose();
        }
    }
}
