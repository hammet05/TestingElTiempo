using System.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace JobBoard.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _ctx;
        private readonly DbSet<T> _set;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _set = _ctx.Set<T>();
        }

        public IQueryable<T> Query() => _set;

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate) => _set.Where(predicate);

        public T Find(params object[] keyValues) => _set.Find(keyValues);

        public T FirstOrDefault(Expression<Func<T, bool>> predicate) => _set.FirstOrDefault(predicate);

        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _set.Add(entity);
        }

        public void Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (_ctx.Entry(entity).State == EntityState.Detached) _set.Attach(entity);
            _set.Remove(entity);
        }

        public void Attach(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _set.Attach(entity);
        }

        public void Detach(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _ctx.Entry(entity).State = EntityState.Detached;
        }
    }
}
