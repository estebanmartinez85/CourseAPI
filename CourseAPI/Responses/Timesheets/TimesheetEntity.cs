using System.Collections.Generic;
using CourseAPI.Models;
using CourseAPI.Responses.Courses;
using CourseAPI.Responses.TimesheetTask;
using FluentSiren.Builders;

namespace CourseAPI.Responses.Timesheets {
    public class TimesheetEntity : BaseSirenEntity {
        private Timesheet _timesheet;

        public TimesheetEntity(Timesheet timesheet, List<Course> courses, List<Models.TimesheetTask> tasks) {
            _timesheet = timesheet;
            this.WithClass("timesheet")
                .WithProperty("id", timesheet.Id)
                .WithProperty("beginDate", timesheet.BeginDate)
                .WithProperty("endDate", timesheet.EndDate)
                .WithSubEntity(new TimesheetTasksResponse(tasks))
                .WithSubEntity(new AssignedCoursesResponse(courses));
            
            EmbeddedRepresentationBuilder rows = new EmbeddedRepresentationBuilder()
                .WithClass("timesheetrow")
                .WithClass("collection")
                .WithRel("rows")
                .WithProperty("count", timesheet.Rows.Count);
            foreach (TimesheetRow row in timesheet.Rows) {
                rows.WithSubEntity(new EmbeddedRepresentationBuilder()
                    .WithClass("timesheetrow")
                    .WithProperty("courseId", row.CourseId)
                    .WithProperty("taskId", row.TaskId)
                    .WithProperty("times", row.Times)
                    .WithProperty("totalHours", row.TotalHours)
                    .WithRel("row")
                );
                
            }
            this.WithSubEntity(rows);
        }
    }
}