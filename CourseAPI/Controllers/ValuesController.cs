﻿using System.Threading.Tasks;
using Courses.Data.Common.Repos;
using Courses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : Controller
    {
        private IRepository<Course> _courses;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public ValuesController(IRepository<Course> courses, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _courses = courses;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //List<Course> courses = _courses.All().ToList();
            //var user = new ApplicationUser { UserName = "mikloedm@gmail.com", Email = "mikloedm@gmail.com" };
            //var resultUser = await _userManager.CreateAsync(user, "1!Qaaaaaa");
            //await _roleManager.CreateAsync(new ApplicationRole("Administrator"));
            //var user2 = await _userManager.FindByEmailAsync("mikloedm@gmail.com");
            //var results = await _userManager.AddToRoleAsync(user2, "Administrator");
            return Ok(new { Test = Enumeration.FromValue<CourseStatus>(1).GetType() });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
