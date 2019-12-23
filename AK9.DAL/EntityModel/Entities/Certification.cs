using System;

namespace AK9.DAL.EntityModel.Entities
{
    public partial class Certification
    {
        public int CertificationId { get; set; }
        public string CertificationName { get; set; }
        public string CertificationImage { get; set; }
        public string CertificationUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
