using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : class {
        internal readonly DbContext Context;

        public Repository(DbContext context) {  
            Context = context;
        }

        public void Add(T entity) {
            Context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities) {
            Context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) {
            return Context.Set<T>().Where(predicate);
        }
        
        public T? Get(Guid id) {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll() {
            return Context.Set<T>().ToList();
        }

        public void Remove(T entity) {
            Context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities) {
            Context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
    }
}
