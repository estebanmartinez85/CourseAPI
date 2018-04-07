using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseAPI.Services {
    public class AccountsService {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsService(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }
        
        public async Task<IList<ApplicationUser>> UsersInRole(string role) {
            IList<ApplicationUser> users = await  _userManager.GetUsersInRoleAsync(role);
            return users;
        }
    }
}