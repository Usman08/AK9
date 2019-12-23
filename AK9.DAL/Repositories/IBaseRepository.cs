using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetAsync(int ID, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(T obj, CancellationToken cancellationToken = default(CancellationToken));
        void Delete(T obj);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
