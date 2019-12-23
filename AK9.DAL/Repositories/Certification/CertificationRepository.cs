using AK9.DAL.EntityModel;
using AK9.DAL.EntityModel.Entities;

namespace AK9.DAL.Repositories
{
    public class CertificationRepository : BaseRepository<Certification>, ICertificationRepository
    {
        private readonly AK9Context _context;

        public CertificationRepository(AK9Context context) : base(context)
        {
            _context = context;
        }
    }
}
