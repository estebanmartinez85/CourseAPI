using CourseAPI.DTO.Course;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseAPI.Extensions;
using CourseAPI.Helpers;
using CourseAPI.Responses.Courses;
using CourseAPI.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CourseAPI.Models;

namespace CourseAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrator,Writer,Narrator,Artist")]
    public class CoursesController : Controller
    {
        private readonly CoursesService _courses;
        private ILogger _logger;

        public CoursesController(CoursesService courses, ILogger<CoursesController> logger)
        {
            _courses = courses;
            _logger = logger;
        }

        [HttpGet("{id}", Name = nameof(GetCourse))]
        public IActionResult GetCourse([FromRoute] int id)
        {
            Course course = _courses.GetCourse(id);
            
            string userId = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string role = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role).Value;

            if (course != null && 
                (role == "Administrator" ||
                 course.CourseUsers.Any(cu => cu.UserId == userId)))
                return Ok(course.Status.GetResponse(this, course).EntityToJson());

            return new NotFoundResult();
        }

        [HttpGet("{id}/Users")]
        [Authorize(Roles = "Administrator")]
        public  IActionResult AssignedToCourse([FromRoute] int id) {
            return Ok(_courses.GetAssignedUsers(id));
        }


        [HttpGet(Name = "AssignedCourses")]
        public IActionResult Assigned()
        {
            string id = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string role = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role).Value;

            List<Course> assigned = role == "Administrator" ? _courses.GetAllCourses() : _courses.GetAssigned(id);

            return Ok(new AssignedCoursesResponse(assigned).EntityToJson());
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CoursesEditDTO model)
        {
            if (!ModelState.IsValid) return new BadRequestResult();

            try
            {
                Course course = await _courses.UpdateBasic(id, model.Code, model.Title, model.LibraryId);
                return Ok(course);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(422);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            bool success = await _courses.Delete(id);
            if (success)
                return Ok();
            return new NotFoundResult();
        }

        [HttpPatch("{id}/Assign")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AssignUser([FromRoute] int id, [FromBody] AssignUserDTO model) {
            if (!ModelState.IsValid) return new BadRequestResult();
            bool success = await _courses.AssignUser(id, model.UserId);
            if (success)
                return Ok();
            return new BadRequestResult();
        }

        //Create
        [HttpPost("")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] CoursesAddDTO model)
        {
            if (!ModelState.IsValid) return new BadRequestResult();

            Course course = await _courses.Create(model.Code, model.Title, model.LibraryId);
            if (course != null)
                return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, null);
            return new BadRequestResult();
        }

        //AssignWriter
        [HttpPatch("{id}/AssignWriter")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AssignWriter([FromRoute] int id, [FromBody] AssignWriterDTO model) {
            try {
                Course course = await _courses.AssignWriter(id, model.WriterId.ToString()); //TODO: Make all these functional
                return Ok(course.Status.GetResponse(this, course).EntityToJson());
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return new BadRequestResult();
            }
        }

        //ScheduleWriterMeeting
        [HttpPatch("{id}/ScheduleWriterMeeting")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ScheduleWriterMeeting([FromRoute] int id, [FromBody] ScheduleWriterMeetingDTO model) {
            if (!ModelState.IsValid) return new BadRequestResult();
            
            try {
                Course course = await _courses.ScheduleWriterMeeting(id, model.Date);
                return Ok(course.Status.GetResponse(this, course).EntityToJson());
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return new BadRequestResult();
            }
        }

        //SetMeetingComplete
        [HttpPatch("{id}/WriterMeetingWaiting")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> WriterMeetingWaiting([FromRoute] int id) {
            try {
                Course course = await _courses.WriterMeetingWaiting(id);
                return Ok(course.Status.GetResponse(this, course).EntityToJson());
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return new BadRequestResult();
            }
        }

        //SetStoryBoardComplete
        [HttpPatch("{id}/StoryboardReadyForReview")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> StoryboardReadyForReview([FromRoute] int id) {
            try {
                Course course = await _courses.StoryboardReadyForReview(id);
                return Ok(course.Status.GetResponse(this, course).EntityToJson());
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return new BadRequestResult();
            }
        }
    }
}
