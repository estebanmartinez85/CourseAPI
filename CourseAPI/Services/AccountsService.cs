using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseAPI.Services {
    public class AccountsService {
        private readonly UserManager<ApplicationUser> _userManager;
        private IRepository<ApplicationUser> _users;

        public AccountsService(UserManager<ApplicationUser> userManager, IRepository<ApplicationUser> users) {
            _userManager = userManager;
            _users = users;
        }

        public async Task<ApplicationUser> UserInfo(string id) {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IList<ApplicationUser>> GetAssignableUsers() {
            List<ApplicationUser> users = new List<ApplicationUser>();
            users.AddRange(await UsersInRole("Writer"));
            users.AddRange(await UsersInRole("Artist"));
            users.AddRange(await UsersInRole("Narrator"));
            return users;
        }
        
        public async Task<IList<ApplicationUser>> UsersInRole(string role) {
                IList<ApplicationUser> users = await  _userManager.GetUsersInRoleAsync(role);
                return users;
            }
        }
}