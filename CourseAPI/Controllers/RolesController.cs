using CourseAPI.DTO.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Data.UOW;
using CourseAPI.Models;

namespace CourseAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private IUowData _data;
        private RoleManager<ApplicationRole> _roleManager;
        public RolesController(IUowData data, RoleManager<ApplicationRole> roleManager)
        {
            _data = data;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult All()
        {
            List<ApplicationRole> roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetIdByName(string name)
        {
            ApplicationRole role = await _roleManager.FindByNameAsync(name);

            if (role == null)
                return new BadRequestResult();

            int id = Int32.Parse(await _roleManager.GetRoleIdAsync(role));
            return Ok(new { id });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNameById(string id)
        {
            ApplicationRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return new BadRequestResult();

            string Name = await _roleManager.GetRoleNameAsync(role);
            return Ok(new { Name });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RolesAddDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new ApplicationRole(model.Name));
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return new BadRequestResult();
        }
    }
}
