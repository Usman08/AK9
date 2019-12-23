using System.ComponentModel.DataAnnotations;

namespace AK9.AppHelper.Models
{
    public class AskAQuoteModel
    {
        [Required]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        public string Company { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CityStateZip { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
