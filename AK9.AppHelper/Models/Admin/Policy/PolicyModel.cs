using System.ComponentModel.DataAnnotations;

namespace AK9.AppHelper.Models
{
    public class PolicyModel : BaseModel
    {
        public int PolicyId { get; set; }
        [Required]
        [Display(Name = "Policy Name")]
        [MaxLength(50)]
        public string PolicyName { get; set; }
        [Display(Name = "Policy File")]
        public string PolicyFile { get; set; }
    }
}
