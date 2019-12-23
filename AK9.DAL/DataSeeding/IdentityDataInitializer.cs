using AK9.DAL.EntityModel;
using AK9.DAL.EntityModel.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace AK9.DAL.DataSeeding
{
    public class IdentityDataInitializer
    {
        private static AK9Context _context;

        public IdentityDataInitializer(AK9Context context)
        {
            _context = context;
        }

        public static void SeedRoles(RoleManager<Role> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                Role role = new Role();
                role.Name = "Admin";
                //role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin@ak9.com").Result == null)
            {
                User user = new User
                {
                    UserName = "admin@ak9.com",
                    Email = "admin@ak9.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssword123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
    }
}
