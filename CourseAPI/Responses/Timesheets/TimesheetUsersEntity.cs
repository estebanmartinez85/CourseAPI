using System.Collections.Generic;
using CourseAPI.Models;

namespace CourseAPI.Responses.Timesheets {
    public class TimesheetUsersEntity : BaseSirenEntity {
        private List<Models.TimesheetTask> _tasks;
        private Timesheet _ts;
        private List<Course> _courses;

        public TimesheetUsersEntity(Timesheet ts, List<Course> courses, List<Models.TimesheetTask> tasks) {
            _ts = ts;
            _courses = courses;
            _tasks = tasks;
            
        }
    }
}