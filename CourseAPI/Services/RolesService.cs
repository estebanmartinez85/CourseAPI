using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseAPI.Services {
    public class RolesService {
        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public RolesService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) {
            _roleManager = roleManager;
            _userManager = userManager;
        }
    }
}