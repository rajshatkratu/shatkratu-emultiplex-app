using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        Task AddAsync(T t);
        Task DeleteAsync(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task UpdateAsync(T t, object key);
    }
}
