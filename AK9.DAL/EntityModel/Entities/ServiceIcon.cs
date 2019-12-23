using System.Collections.Generic;

namespace AK9.DAL.EntityModel.Entities
{
    public partial class ServiceIcon
    {
        public ServiceIcon()
        {
            Service = new HashSet<Service>();
        }

        public int ServiceIconId { get; set; }
        public string ServiceIconName { get; set; }

        public virtual ICollection<Service> Service { get; set; }
    }
}
