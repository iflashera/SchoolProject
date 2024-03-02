using Microsoft.AspNetCore.Identity;
using Models.Models.Identity;
using Models.Models.Roles;
using Models.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class DataInitializer
    {
        public static void SeedApplicationRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Admin";
                role.Description = "Manage School";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Teacher").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Teacher";
                role.Description = "Manage Classes";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Parent").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Parent";
                role.Description = "View Child Grades";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Student";
                role.Description = "View Assignments";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
        public static void SeedRoles(RoleManager<ApplicationRole> roleManager, SmsDbContext dbContext)
        {
            Role r;
            var role1 = roleManager.FindByNameAsync("Admin").Result;
            if (role1 != null)
            {
                var rl = dbContext.Roles.FirstOrDefault(x => x.ApplicationRoleId == role1.Id);
                if (rl == null)
                {
                    r = new Role();
                    r.ApplicationRoleId = role1.Id;
                    r.RoleName = role1.Name;
                    r.CreatedBy = 1;
                    r.CreatedDate = DateTime.Now;
                    r.UpdatedBy = 1;
                    r.UpdatedDate = DateTime.Now;
                    var res = dbContext.Roles.Add(r);
                    dbContext.SaveChanges();
                }
            }
            var role2 = roleManager.FindByNameAsync("Teacher").Result;
            if (role2 != null)
            {
                var rl = dbContext.Roles.FirstOrDefault(x => x.ApplicationRoleId == role2.Id);
                if (rl == null)
                {
                    r = new Role();
                    r.ApplicationRoleId = role2.Id;
                    r.RoleName = role2.Name;
                    r.CreatedBy = 1;
                    r.CreatedDate = DateTime.Now;
                    r.UpdatedBy = 1;
                    r.UpdatedDate = DateTime.Now;
                    dbContext.Roles.Add(r);
                    dbContext.SaveChanges();
                }
            }
            var role3 = roleManager.FindByNameAsync("Student").Result;
            {
                var rl = dbContext.Roles.FirstOrDefault(x => x.ApplicationRoleId == role3.Id);
                if (rl == null)
                {
                    r = new Role();
                    r.ApplicationRoleId = role3.Id;
                    r.RoleName = role3.Name;
                    r.CreatedBy = 1;
                    r.CreatedDate = DateTime.Now;
                    r.UpdatedBy = 1;
                    r.UpdatedDate = DateTime.Now;
                    dbContext.Roles.Add(r);
                    dbContext.SaveChanges();
                }
            }
            var role4 = roleManager.FindByNameAsync("Parent").Result;
            {
                var rl = dbContext.Roles.FirstOrDefault(x => x.ApplicationRoleId == role4.Id);
                if (rl == null)
                {
                    r = new Role();
                    r.ApplicationRoleId = role4.Id;
                    r.RoleName = role4.Name;
                    r.CreatedBy = 1;
                    r.CreatedDate = DateTime.Now;
                    r.UpdatedBy = 1;
                    r.UpdatedDate = DateTime.Now;
                    dbContext.Roles.Add(r);
                    dbContext.SaveChanges();
                }
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, SmsDbContext dbContext, RoleManager<ApplicationRole> roleManager)
        {

            // Seed  Admin
            if (userManager.FindByNameAsync("Ifla@yopmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Ifla@yopmail.com";
                user.Email = "ifla@yopmail.com";
                user.FirsName = "Ifla";
                user.LastName = "Shera";

                IdentityResult result = userManager.CreateAsync
                (user, "Password@1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();

                    var userM = userManager.FindByEmailAsync("ifla@yopmail.com").Result;
                    var userEntry = new UserDetail
                    {
                        ApplicationUserId = Convert.ToString(userM.Id),
                        RoleId = dbContext.Roles.FirstOrDefault(r => r.ApplicationRoleId == roleManager.FindByNameAsync("Admin").Result.Id).Id,
                        isActive = true,
                    };
                    dbContext.UserDetails.Add(userEntry);
                    dbContext.SaveChanges();
                    var dbUser = dbContext.UserDetails.FirstOrDefault(r => r.ApplicationUserId == userEntry.ApplicationUserId);

                    var adm = new Admin
                    {
                        FirstName = "Ifla",
                        LastName = "Shera",
                        CreatedBy = null,
                        UpdatedBy = null,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        UserId = dbUser.Id,
                        UserName = userM.UserName,
                    };

                    dbContext.Admins.Add(adm);
                    dbContext.SaveChanges();
                }


            }
            var UManAdmin = userManager.FindByEmailAsync("ifla@yopmail.com").Result;
            var dbUserDetailAdmin = dbContext.UserDetails.FirstOrDefault(r => r.ApplicationUserId == UManAdmin.Id);
            if (dbUserDetailAdmin == null)
            {
                var userEntry = new UserDetail
                {
                    ApplicationUserId = Convert.ToString(UManAdmin.Id),
                    RoleId = dbContext.Roles.FirstOrDefault(r => r.ApplicationRoleId == roleManager.FindByNameAsync("Ifla").Result.Id).Id,
                    isActive = true,
                };
                dbContext.UserDetails.Add(userEntry);
                dbContext.SaveChanges();
                var dbUser = dbContext.UserDetails.FirstOrDefault(r => r.ApplicationUserId == userEntry.ApplicationUserId);

                var adm = new Admin
                {
                    FirstName = "Ifla",
                    LastName = "Shera",
                    CreatedBy = null,
                    UpdatedBy = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UserId = dbUser.Id,
                    UserName = UManAdmin.UserName
                };
                dbContext.Admins.Add(adm);

                dbContext.SaveChanges();
            }
        }

       public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
          SmsDbContext dbContext)
        {
            SeedApplicationRoles(roleManager);
            SeedRoles(roleManager, dbContext);            
            SeedUsers(userManager, dbContext, roleManager);

        }

    }
   
}
