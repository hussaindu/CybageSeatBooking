using CybageSeatBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace CybageSeatBooking.Service
{
    public class DataIntilizer
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser>? userManager,
                                          RoleManager<IdentityRole>? roleManager)
        {
            if(userManager == null && roleManager == null)
            {
                Console.WriteLine("usermanager and role manager does not exists");
                return;
            }

            //admin role created
            var exists = await roleManager.RoleExistsAsync("admin");
            if (!exists)
            {
                Console.WriteLine("admin role is not defined and will created");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            //employee

            exists = await roleManager.RoleExistsAsync("employee");
            if (!exists)
            {
                Console.WriteLine("employee role is not defined will be created");
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }

            //checking multiples admin are there

            var adminUsers = await userManager.GetUsersInRoleAsync("admin");
            if (adminUsers.Any()) 
            {
                Console.WriteLine("Admin users already exists => exists");
                return;
            }

            //creating admin credetionals

            var user = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                CreateAt = DateTime.Now,
            };
            string initialPassword = "admin123";

            var result = await userManager.CreateAsync(user, initialPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
                Console.WriteLine("admin user created");
                Console.WriteLine("Email " + user.Email);
                Console.WriteLine("InitialPassword :" + initialPassword);
            }
        }
    }
}
