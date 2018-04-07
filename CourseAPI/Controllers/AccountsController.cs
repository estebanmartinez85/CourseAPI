using CourseAPI.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CourseAPI.DTO.User;
using CourseAPI.Helpers;
using CourseAPI.Models;
using CourseAPI.Responses.Accounts;
using CourseAPI.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CourseAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class AccountsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AccountsService _accountsService;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            AccountsService accountsService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _accountsService = accountsService;
        }

        [HttpPost("{Id}/Role")]
        public async Task<IActionResult> Role([FromRoute] string Id, [FromBody] AccountsRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.RoleName);
                if (role != null)
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(Id);

                    var result = await AddUserToRoleAsync(user, model.RoleName);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return new BadRequestResult();
                    }
                } else
                {
                    return StatusCode(422, new { Error = role + " does not exist." });
                }
            }

            return new BadRequestResult();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<object> Login([FromBody] AccountsLoginDTO model)
        {
            if (!ModelState.IsValid) return new BadRequestResult();
            
            SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded) return StatusCode(403, new {error = "Invalid email or password."});
            
            ApplicationUser appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            IList<string> role = await _userManager.GetRolesAsync(appUser);
            UserDTO user = new UserDTO {
                FirstName = appUser?.FirstName ?? "",
                Role = role[0] ?? ""
            };
            if (appUser != null) {
                return new {
                    Token = await GenerateJwtToken(model.Email, appUser),
                    ExpiresIn = 86400,
                    FirstName = user.FirstName,
                    Role = user.Role
                };
            }
            else {
                return new { error="No Such User" };
            }
        }

        [HttpGet("{role}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UsersInRole([FromRoute] string role) {
            IList<ApplicationUser> usersList = await _accountsService.UsersInRole(role);
            
            if (usersList.Count <= 0) return new BadRequestResult();
            
            return Ok(new AccountsResponse(this, new ReadOnlyCollection<ApplicationUser>(usersList)).EntityToJson());
        }

        private async Task<string> GenerateJwtToken(string email, ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            if (userClaims.Count > 0)
            {
                claims.AddRange(userClaims);
            }
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                claims.Add(new Claim(ClaimTypes.Role, roles[0]));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Secret"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Auth:ExpireDays"]));

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Auth:Issuer"],
                _configuration["Auth:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AccountsAddDTO model)
        {
            if (!ModelState.IsValid) return new BadRequestResult();

            ApplicationRole role = await _roleManager.FindByNameAsync(model.Role);
            if (role == null) return StatusCode(422, new { Error = "Role Not Found" });
            
            ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded) return new BadRequestResult();
            
            ApplicationUser currentUser = await _userManager.FindByEmailAsync(model.Email);
            IdentityResult roleResult = await _userManager.AddToRoleAsync(currentUser, model.Role);

            return Ok();
        }

        [HttpGet]
        public IActionResult List()
        {
            List<string> users = (from u in _userManager.Users
                                  select u.Email).ToList();
            return Ok(users);
        }
        
        
        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] AccountsEditDTO model)
        {
            if (!ModelState.IsValid) return new BadRequestResult();
            
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            IdentityResult roleResult = await AddUserToRoleAsync(user, model.RoleName);
            
            if (!roleResult.Succeeded) return new BadRequestResult();
            
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            IdentityResult result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? Ok() as IActionResult : new BadRequestResult();
        }

        private async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            IList<string> currentRoles = await _userManager.GetRolesAsync(user);

            if(currentRoles.Count > 0)
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}