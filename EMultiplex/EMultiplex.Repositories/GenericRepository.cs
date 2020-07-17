using EMultiplex.DAL;
using EMultiplex.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected EMultiplexDbContext Context { get; private set; }

        protected GenericRepository(EMultiplexDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task AddAsync(T t)
        {
            await Context.Set<T>().AddAsync(t);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await Task.FromResult(Context.Set<T>().Remove(entity));
        }

        
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual async  Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        
        public virtual async Task UpdateAsync(T t, object key)
        {
            T exist = await Context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(t);
            }
        }
    }
}
