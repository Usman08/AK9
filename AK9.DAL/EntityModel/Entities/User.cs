using Microsoft.AspNetCore.Identity;

namespace AK9.DAL.EntityModel.Entities
{
    public class User : IdentityUser<int>
    {
        public string Answer { get; set; }
        public bool IsActive { get; set; }     
    }
}
