using System;
using CourseAPI.Data.Common.Models;

namespace CourseAPI.Models {
    public class TimesheetTask : BaseModel {
        public int Id { get; set; }
        public string Name { get; set; }

        protected TimesheetTask() {
        }

        public TimesheetTask(string name) {
            Name = name;
        }
    }
}