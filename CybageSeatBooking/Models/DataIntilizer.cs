using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace CybageSeatBooking.Models
{
    public class DataIntilizer
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser>? userManager,
                                               RoleManager<IdentityRole>? roleManager)
        {
            if (userManager == null)
            {
                Console.WriteLine("UserManager does not exist");
                return;
            }

            if (roleManager == null)
            {
                Console.WriteLine("RoleManager does not exist");
                return;
            }

            // Admin role
            var exists = await roleManager.RoleExistsAsync("admin");
            if (!exists)
            {
                Console.WriteLine("admin role is not defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            // Employee role
            exists = await roleManager.RoleExistsAsync("employee");
            if (!exists)
            {
                Console.WriteLine("employee role is not defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }

            // Check if any admin user already exists
            var adminUsers = await userManager.GetUsersInRoleAsync("admin");
            if (adminUsers.Any())
            {
                Console.WriteLine("Admin users already exist.");
                return;
            }

            // Create default admin user
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
                Console.WriteLine("Admin user created.");
                Console.WriteLine("Email: " + user.Email);
                Console.WriteLine("Initial Password: " + initialPassword);
            }
        }
    }

}
