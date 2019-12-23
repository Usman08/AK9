using System.ComponentModel.DataAnnotations;

namespace AK9.AppHelper.Models
{
    public class CertificationModel : BaseModel
    {
        public int CertificationId { get; set; }
        [Required]
        [Display(Name ="Certification Name")]
        [MaxLength(50)]
        public string CertificationName { get; set; }
        [Display(Name = "Certification Image")]
        public string CertificationImage { get; set; }
        [Display(Name = "Certification Url")]
        [MaxLength(100)]
        public string CertificationUrl { get; set; }
    }
}
