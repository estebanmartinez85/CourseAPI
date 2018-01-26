using CourseAPI.DTO.Account;
using Courses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded) 
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    return await GenerateJwtToken(model.Email, appUser);
                }
                return StatusCode(403, new { error = "Invalid email or password." });
            }
            return new BadRequestResult();
        }

        private async Task<object> GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var userClaims = await _userManager.GetClaimsAsync(user);
            if (userClaims.Count > 0)
            {
                claims.AddRange(userClaims);
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                claims.Add(new Claim(ClaimTypes.Role, roles[0]));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Auth:ExpireDays"]));

            var token = new JwtSecurityToken(
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
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.Role);
                if (role != null)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var currentUser = await _userManager.FindByEmailAsync(model.Email);
                        var roleResult = await _userManager.AddToRoleAsync(currentUser, model.Role);

                        if (roleResult.Succeeded)
                        {
                            return Ok();
                        }
                    }
                } else
                {
                    return StatusCode(422, new { Error = "Role Not Found" });
                }
            }

            return new BadRequestResult();
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
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                var roleResult = await AddUserToRoleAsync(user, model.RoleName);
                if (roleResult.Succeeded)
                {
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }

            }
            return new BadRequestResult();
        }

        private async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);

            if(currentRoles.Count > 0)
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}