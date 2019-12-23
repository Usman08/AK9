using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.BLL.Services
{
    public interface IBaseBLL<T, TSearch, TId>
    {
        Task<List<T>> GetListAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<List<T>> GetListAsync(TSearch searchModel, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetAsync(TId id, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveAsync(T model, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> UpdateAsync(T model, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> DeleteAsync(TId Id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
