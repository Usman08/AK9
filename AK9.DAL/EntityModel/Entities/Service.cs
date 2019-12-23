using System;

namespace AK9.DAL.EntityModel.Entities
{
    public partial class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int ServiceIconId { get; set; }
        public string BannerImage { get; set; }
        public string Heading { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ServiceIcon ServiceIcon { get; set; }
    }
}
