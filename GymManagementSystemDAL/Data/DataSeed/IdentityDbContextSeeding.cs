using GymManagementSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.DataSeed
{
    public class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUsers = userManager.Users.Any();
                var HasRoles = roleManager.Roles.Any();

                if (HasUsers && HasRoles) return false;

                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>()
                {
                    new () { Name = "Admin"},
                    new () { Name = "SuperAdmin"},
                };

                    foreach (var role in Roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name!).Result)
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                    }
                }

                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser
                    {
                        FirstName = "Mark",
                        LastName = "Sameeh",
                        UserName = "MarkSameeh",
                        Email = "mark.sameeh@gmail.com",
                        PhoneNumber = "01000000000",

                    };
                    userManager.CreateAsync(MainAdmin, "P@ssw0rd!").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser
                    {
                        FirstName = "Mohamed",
                        LastName = "Ahmed",
                        UserName = "MohamedAhmed",
                        Email = "Mohamed.Ahmed@gmail.com",
                        PhoneNumber = "01100956000",

                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd!").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed : {ex}");
                return false;
            }
        }
    }
}
