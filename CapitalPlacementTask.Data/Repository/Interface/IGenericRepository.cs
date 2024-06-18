using CapitalPlacementTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Data.Repository.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task DeleteAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<T> GetOneAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task InsertAsync(T entity);
        Task<bool> InsertMultipleAsync(IEnumerable<T> entities);
        void Reload(ref T entity);
        Task SoftDeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        void UpdateRange(IEnumerable<T> entities);

    }
}
