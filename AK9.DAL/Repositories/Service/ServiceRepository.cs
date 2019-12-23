using AK9.DAL.EntityModel;
using AK9.DAL.EntityModel.Entities;

namespace AK9.DAL.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        private readonly AK9Context _context;

        public ServiceRepository(AK9Context context) : base(context)
        {
            _context = context;
        }
    }
}
