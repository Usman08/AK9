using AK9.DAL.EntityModel;
using AK9.DAL.EntityModel.Entities;

namespace AK9.DAL.Repositories
{
    public class PolicyRepository : BaseRepository<Policy>, IPolicyRepository
    {
        private readonly AK9Context _context;

        public PolicyRepository(AK9Context context) : base(context)
        {
            _context = context;
        }
    }
}
