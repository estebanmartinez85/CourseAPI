using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;

namespace CourseAPI.Services {
    public class TasksService {
        private IRepository<TimesheetTask> _tasks;

        public TasksService(IRepository<TimesheetTask> tasks) {
            _tasks = tasks;
        }

        public List<TimesheetTask> GetTasks() {
            return _tasks.All().ToList();
        }

        public async Task<TimesheetTask> AddTask(string modelName) {
            TimesheetTask task = new TimesheetTask(modelName);
            _tasks.Add(task);
            await _tasks.SaveChangesAsync();
            return task;
        }
    }
}