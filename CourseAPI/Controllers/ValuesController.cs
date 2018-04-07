using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CourseAPI.Models;

namespace CourseAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : Controller
    {
        private IRepository<Course> _courses;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private ILogger<ValuesController> _logger;

        public ValuesController(IRepository<Course> courses, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<ValuesController> logger)
        {
            _courses = courses;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }


        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            //List<Course> courses = _courses.All().ToList();
            //var user = new ApplicationUser { UserName = "mikloedm@gmail.com", Email = "mikloedm@gmail.com" };
            //var resultUser = await _userManager.CreateAsync(user, "1!Qaaaaaa");
            //await _roleManager.CreateAsync(new ApplicationRole("Administrator"));
            //var user2 = await _userManager.FindByEmailAsync("mikloedm@gmail.com");
            //var results = await _userManager.AddToRoleAsync(user2, "Administrator");

            //var user = new ApplicationUser { UserName = "mikloedm2@gmail2.com", Email = "mikloedm2@gmail2.com" };
            //var resultUser = await _userManager.CreateAsync(user, "1!Qaaaaaa");
            //await _roleManager.CreateAsync(new ApplicationRole("Writer"));
            //var user2 = await _userManager.FindByEmailAsync("mikloedm2@gmail2.com");
            //var results = await _userManager.AddToRoleAsync(user2, "Writer");

            var user = new ApplicationUser { UserName = "mikloedm3@gmail2.com", Email = "mikloedm3@gmail2.com" };
            var resultUser = await _userManager.CreateAsync(user, "1!Qaaaaaa");
            await _roleManager.CreateAsync(new ApplicationRole("Writer"));
            var user2 = await _userManager.FindByEmailAsync("mikloedm3@gmail2.com");
            var results = await _userManager.AddToRoleAsync(user2, "Writer");
            
            return Ok(new {test=4});
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) {
            _logger.LogWarning("WARNING");
            return HttpContext.Request.Path;
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
