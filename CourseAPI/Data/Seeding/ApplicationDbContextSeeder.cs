using CourseAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI;

namespace CourseAPI.Data.Seeding
{
    class ApplicationDbContextSeeder
    {
        public void Seed(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            this.Seed(dbContext, roleManager, userManager);
        }

        public void Seed(ApplicationDbContext dbContext, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.SeedRoles(roleManager);
            this.SeedAdmin(userManager);
        }

        private void SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            this.SeedAdmin(GlobalConstants.AdministratorRoleName, userManager);
        }

        private void SeedAdmin(string administratorUserName, UserManager<ApplicationUser> userManager)
        {
            var admin = userManager.FindByNameAsync(administratorUserName).GetAwaiter().GetResult();
            if (admin == null)
            {
                var user = new ApplicationUser { UserName = GlobalConstants.AdministratorUserName, Email = GlobalConstants.AdministratorUserName };
                var resultUser = userManager.CreateAsync(user, GlobalConstants.AdministratorUserName + "1qaz").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName).GetAwaiter().GetResult();
            }
        }

        private void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            this.SeedRole(GlobalConstants.AdministratorRoleName, roleManager);
        }

        private void SeedRole(string roleName, RoleManager<ApplicationRole> roleManager)
        {
            var role = roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult();
            if (role == null)
            {
                var result = roleManager.CreateAsync(new ApplicationRole(roleName)).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
