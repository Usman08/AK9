using System.ComponentModel.DataAnnotations;

namespace AK9.AppHelper.Models
{
    public class ContactUsModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}
