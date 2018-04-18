using System.Collections.Generic;
using System.Threading.Tasks;
using CourseAPI.DTO.Tasks;
using CourseAPI.Helpers;
using CourseAPI.Models;
using CourseAPI.Responses.TimesheetTask;
using CourseAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace CourseAPI.Controllers {
    [Authorize(Roles = "Administrator")]
    public class TimesheetTasksController : Controller {
        private readonly TasksService _tasks;

        public TimesheetTasksController(TasksService tasks) {
            _tasks = tasks;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTask() {
            List<TimesheetTask> tasks = _tasks.GetTasks();
            TimesheetTasksResponse tasksResponse = new TimesheetTasksResponse(tasks);
            return Ok(tasksResponse.EntityToJson());
        }
        
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskAddDTO model) {
            if (!ModelState.IsValid) return new BadRequestResult();
            TimesheetTask task = await _tasks.AddTask(model.Name);
            TimesheetTaskEntity entity = new TimesheetTaskEntity(task);
            return Ok(entity.EntityToJson());
        }
    }
}