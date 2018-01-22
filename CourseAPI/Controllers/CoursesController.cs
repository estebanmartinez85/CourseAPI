using CourseAPI.Data.Common.Repos;
using CourseAPI.Data.UOW;
using CourseAPI.DTO.Course;
using CourseAPI.Extensions;
using CourseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator,Writer,Narrator,Artist")]
    public class CoursesController : Controller
    {
        protected IRepository<Course> _courses;
        protected IUowData _data;
        protected UserManager<ApplicationUser> _userManager;
        protected RoleManager<ApplicationRole> _roleManager;

        public CoursesController(IUowData data, IRepository<Course> courses, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _data = data;
            _userManager = userManager;
            _roleManager = roleManager;
            _courses = courses;
        }
        [HttpGet]
        public IActionResult All()
        {
            List<Course> courses = _courses.All().ToList();
            if (courses.Count == 0)
                return new NoContentResult();

            return Ok(courses);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CoursesAddDTO model)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    Title = model.Title,
                    Code = model.Code,
                    Status = "Created",
                    LibraryId = model.LibraryId,
                    Storyboard = new Storyboard(),
                    CourseUsers = new List<CourseUsers>()
                };
                _courses.Add(course);
                await _courses.SaveChangesAsync();

                return Ok();
            }
            return new BadRequestResult();
        }

        [HttpGet("{Id}/Assigned")]
        public IActionResult Assigned([FromRoute] int Id)
        {
            var users = _data.CourseUsers.All().Include(cu => cu.User).Where(c => c.CourseId == Id).Select(c => new {
                c.UserId,
                c.User.Email,
                c.User.FirstName,
                c.User.LastName
            }).ToList();
            return Ok(users);
        }
        [HttpPost("{Id}/Assign")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Assign([FromRoute] int Id, [FromBody] CoursesAssignDTO model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.UserId);

                CourseUsers courseUsers = new CourseUsers { CourseId = Id, UserId = model.UserId };
                _data.CourseUsers.Add(courseUsers);
                await _data.CourseUsers.SaveChangesAsync();

                return Ok();
            }
            return new BadRequestResult();
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] CoursesEditDTO model)
        {
            if (ModelState.IsValid)
            {
                Course course = await _courses.GetByIdAsync(Id);
                if (course != null)
                {
                    course.Title = model.Title;
                    course.Code = model.Code;
                    course.GraphicHoursGoal = model.GraphicHoursGoal;
                    course.GraphicHoursActual = model.GraphicHoursActual;
                    course.StoryboardHoursGoal = model.StoryboardHoursGoal;
                    course.StoryboardHoursActual = model.StoryboardHoursActual;
                    course.LibraryId = int.Parse(model.LibraryId);

                    _courses.Update(course);
                    await _courses.SaveChangesAsync();

                    return Ok(course);
                } else
                {
                    return new NotFoundResult();
                }
            }
            return new BadRequestResult();
        }
    }
}
