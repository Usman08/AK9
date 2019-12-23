using AK9.DAL.EntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.DAL.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AK9Context _context;

        public BaseRepository(AK9Context context)
        {
            _context = context;
        }

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int ID, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Set<T>().FindAsync(new object[] { ID }, cancellationToken);
        }

        public async Task AddAsync(T obj, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.Set<T>().AddAsync(obj, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
    }
}
