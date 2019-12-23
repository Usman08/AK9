using AK9.DAL.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        PolicyRepository PolicyRepository { get; }
        CertificationRepository CertificationRepository { get; }
        ServiceRepository ServiceRepository { get; }
        ServiceIconRepository ServiceIconRepository { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
