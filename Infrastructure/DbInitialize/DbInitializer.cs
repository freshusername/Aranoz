using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DbInitialize
{
    public static class DbInitializer
    {
        public static void SeedData(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                 roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExistsAsync("Owner").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Owner"));
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                roleManager.CreateAsync(new IdentityRole("User"));
            }
        }

        public static void SeedUsers(UserManager<AppUser> userManager)
        {
            var admin = new AppUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "+380930000000",
                EmailConfirmed = true
            };

            var owner = new AppUser
            {
                Email = "owner@owner.com",
                UserName = "owner@owner.com",
                FirstName = "Owner",
                LastName = "Owner",
                PhoneNumber = "+380930000000",
                EmailConfirmed = true
            };

            var user = new AppUser
            {
                Email = "nechypor.dan@gmail.com",
                UserName = "nechypor.dan@gmail.com",
                FirstName = "User",
                LastName = "User",
                PhoneNumber = "+380930000000",
                EmailConfirmed = true
            };

            if (userManager.FindByNameAsync(admin.Email).Result == null)
            {
                IdentityResult result;
                result = userManager.CreateAsync(admin, "admin12345").Result;
                
                if (result.Succeeded)              
                    userManager.AddToRoleAsync(admin, "Admin").Wait();             
            }

            if (userManager.FindByNameAsync(owner.Email).Result == null)
            {
                IdentityResult result;
                result = userManager.CreateAsync(owner, "owner12345").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(owner, "Owner").Wait();
            }

            if (userManager.FindByNameAsync(user.Email).Result == null)
            {
                IdentityResult result;
                result = userManager.CreateAsync(user, "user12345").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "User").Wait();
            }
        }
    }
}
