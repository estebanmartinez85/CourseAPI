namespace CourseAPI.Responses.TimesheetTask {
    public class TimesheetTaskEntity : BaseSirenEntity {
        private Models.TimesheetTask _task;

        public TimesheetTaskEntity(Models.TimesheetTask task) {
            _task = task;
            this.WithClass("timesheettask")
                .WithProperty("id", task.Id)
                .WithProperty("name", task.Name);
        }
    }
}