using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AK9.AppHelper.Models
{
    public class ServiceModel : BaseModel
    {
        public int ServiceId { get; set; }
        [Required]
        [Display(Name = "Service Name")]
        [MaxLength(50)]
        public string ServiceName { get; set; }
        [Required]
        [Display(Name = "Service Icon")]
        public int ServiceIconId { get; set; }
        public string ServiceIconName { get; set; }
        [Display(Name = "Banner Image")]
        public string BannerImage { get; set; }
        [Display(Name = "Heading")]
        [MaxLength(100)]
        public string Heading { get; set; }
        [Display(Name = "Short Description")]
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }

        public List<SelectListItem> ServiceIconSelectList { get; set; }
    }
}
