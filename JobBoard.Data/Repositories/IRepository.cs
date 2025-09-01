using System;
using System.Linq;
using System.Linq.Expressions;

namespace JobBoard.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);

        T Find(params object[] keyValues);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Remove(T entity);

        void Attach(T entity);
        void Detach(T entity);
        void Update(T entity);

        void UpdateProperty<TProperty>(T entity, Expression<Func<T, TProperty>> property);

        void UpdateProperties(T entity, params Expression<Func<T, object>>[] properties);

    }
}
