using AK9.DAL.EntityModel;
using AK9.DAL.EntityModel.Entities;

namespace AK9.DAL.Repositories
{
    public class ServiceIconRepository : BaseRepository<ServiceIcon>, IServiceIconRepository
    {
        private readonly AK9Context _context;

        public ServiceIconRepository(AK9Context context) : base(context)
        {
            _context = context;
        }
    }
}
