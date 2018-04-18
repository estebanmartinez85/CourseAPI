using System.Collections.Generic;
using FluentSiren.Builders;

namespace CourseAPI.Responses.TimesheetTask {
    public class TimesheetTasksResponse : EntityBuilder, ISubEntityBuilder {
        private List<Models.TimesheetTask> _tasks;

        public TimesheetTasksResponse(List<Models.TimesheetTask> tasksList) {
            _tasks = tasksList;
            this.WithClass("timesheetstask")
                .WithClass("collection")
                .WithProperty("count", tasksList.Count);
            foreach (Models.TimesheetTask task in tasksList) {
                this.WithSubEntity(new TimesheetTaskEntity(task));
            }
        }
    }
}