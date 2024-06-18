using CapitalPlacementTask.Data.DataAccess;
using CapitalPlacementTask.Data.Models;
using CapitalPlacementTask.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Data.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;


        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query;
            if (includes.Length > 0)
            {
                IIncludableQueryable<T, object> includableQuery = null;
                for (var i = 0; i < includes.Length; i++)
                {
                    includableQuery = includableQuery == null ? _context.Set<T>().Include(includes[i]) : includableQuery.Include(includes[i]);
                }
                query = includableQuery.Where(expression);
            }
            else
            {
                query = _context.Set<T>().Where(expression);
            }
            return query;
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                T entity;
                if (includes.Length > 0)
                {
                    IIncludableQueryable<T, object> includableQuery = null;
                    for (var i = 0; i < includes.Length; i++)
                    {
                        includableQuery = includableQuery == null ? _context.Set<T>().Include(includes[i]) : includableQuery.Include(includes[i]);
                    }
                    entity = await includableQuery.FirstOrDefaultAsync(expression);
                }
                else
                {
                    entity = await _context.Set<T>().FirstOrDefaultAsync(expression);
                }
                return entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                T entity;
                if (includes.Length > 0)
                {
                    IIncludableQueryable<T, object> includableQuery = null;
                    for (var i = 0; i < includes.Length; i++)
                    {
                        includableQuery = includableQuery == null ? _context.Set<T>().Include(includes[i]) : includableQuery.Include(includes[i]);
                    }
                    entity = await includableQuery.SingleOrDefaultAsync(expression);
                }
                else
                {
                    entity = await _context.Set<T>().SingleOrDefaultAsync(expression);
                }
                return entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IEnumerable<T> entities;
                if (includes.Length > 0)
                {
                    IIncludableQueryable<T, object> includableQuery = null;
                    for (var i = 0; i < includes.Length; i++)
                    {
                        includableQuery = includableQuery == null ? _context.Set<T>().Include(includes[i]) : includableQuery.Include(includes[i]);
                    }
                    entities = expression == null ? await includableQuery.ToListAsync() : await includableQuery.Where(expression).ToListAsync();
                }
                else
                {
                    entities = expression == null ? await _context.Set<T>().ToListAsync() : await _context.Set<T>().Where(expression).ToListAsync();
                }
                return entities;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Reload(ref T entity)
        {
            try
            {
                _context.Entry(entity).Reload();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            try
            {
                _context.Set<T>().UpdateRange(entities);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entity = await _context.Set<T>().SingleOrDefaultAsync(expression);
                _context.Remove(entity);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task SoftDeleteAsync(T entity)
        {
            try
            {
                entity.IsDeleted = true;
                entity.DateDeleted = DateTime.Now;
                _context.Set<T>().Update(entity);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

}
