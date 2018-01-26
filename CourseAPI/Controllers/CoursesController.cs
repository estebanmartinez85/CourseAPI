using CourseAPI.DTO.Course;
using Courses.Models;
using Courses.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrator,Writer,Narrator,Artist")]
    public class CoursesController : Controller
    {
        private CoursesService _courses;

        public CoursesController(CoursesService courses)
        {
            _courses = courses;
        }

        [HttpGet("{id}", Name = nameof(GetCourse))]
        public IActionResult GetCourse([FromRoute] int id)
        {
            Course course = _courses.GetCourse(id);
            if (course != null)
                return Ok(course);

            return new NotFoundResult();
        }

        [HttpGet("All", Name = nameof(All))]
        public IActionResult All()
        {
            List<Course> courses = _courses.GetAllCourses();
            if (courses.Count == 0)
                return new NoContentResult();

            return Ok(courses.Select( c =>
                    new
                    {
                        c.LibraryId,
                        c.Code,
                        c.Title
                    }
                ));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CoursesAddDTO model)
        {
            if (ModelState.IsValid)
            {
                Course course = await _courses.AddNewCourse(model.Title, model.Code, model.LibraryId);
                if (course != null)
                    return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, null);
            }
            return new BadRequestResult();
        }

        [HttpGet("{id}/Assigned", Name = nameof(Assigned))]
        public IActionResult Assigned([FromRoute] int id)
        {
            var users = _courses.GetAssignedUsers(id);
            return Ok(users);
        }

        [HttpPost("{id}/Assign")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Assign([FromRoute] int id, [FromBody] CoursesAssignDTO model)
        {
            if (ModelState.IsValid)
            {
                bool result = await _courses.AssignUser(id, model.UserId);
                if(result)
                    return Ok();
            }
            return new BadRequestResult();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CoursesEditDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Course course = await _courses.UpdateBasic(id, model.Code, model.Title, model.LibraryId);
                    return Ok(course);
                } catch (Exception e)
                {
                    return StatusCode(422, new { Error = e.Message });
                }
            }
            return new BadRequestResult();
        }
    }
}
