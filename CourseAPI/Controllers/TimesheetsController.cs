using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseAPI.DTO.Timesheet;
using CourseAPI.Helpers;
using CourseAPI.Models;
using CourseAPI.Responses.Timesheets;
using CourseAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;


namespace CourseAPI.Controllers {
    
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class TimesheetsController : Controller {
        private readonly TimesheetService _service;
        private readonly CoursesService _coursesService;
        private readonly TasksService _tasksService;

        public TimesheetsController(TimesheetService service, CoursesService coursesService, TasksService tasksService) {
            _service = service;
            _coursesService = coursesService;
            _tasksService = tasksService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Writer, Artist, Narrator")]
        public async Task<IActionResult> GetCurrentTimesheet() {
            Timesheet ts =  await _service.GetCurrentTimesheet(HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            string id = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string role = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role).Value;

            List<Course> assigned = role == "Administrator" ? _coursesService.GetAllCourses() : _coursesService.GetAssigned(id);
            List<TimesheetTask> tasks = _tasksService.GetTasks();
            
            TimesheetEntity entity = new TimesheetEntity(ts, assigned, tasks);
            return Ok(entity.EntityToJson());
        }

        [HttpPost("{timesheetId}/row")]
        [Authorize(Roles = "Administrator, Writer, Narrator, Artist")]
        public async Task<IActionResult> AddRows([FromRoute] Guid timesheetId, [FromBody] List<AddRowDTO> model) {
            Timesheet ts = await _service.ClearRows(timesheetId);
            string id = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string role = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role).Value;

            List<Course> assigned = role == "Administrator" ? _coursesService.GetAllCourses() : _coursesService.GetAssigned(id);
            List<TimesheetTask> tasks = _tasksService.GetTasks();

            if (model.Count == 0) {
                ts = await _service.ClearRows(ts.Id);
            }
            
            foreach (AddRowDTO row in model) {
                ts = await _service.AddRow(ts, row.CourseId, row.TaskId, row.Times);
            }
            TimesheetEntity entity = new TimesheetEntity(ts, assigned, tasks);
            return Ok(entity.EntityToJson());
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetTimesheet([FromRoute] string userId) {
            Timesheet ts =  await _service.GetCurrentTimesheet(userId);
            List<Course> courses = _coursesService.GetAssigned(userId);
            List<TimesheetTask> tasks = _tasksService.GetTasks();
            
            TimesheetEntity entity = new TimesheetEntity(ts, courses, tasks);
            return Ok(entity.EntityToJson());
        }
    }
}