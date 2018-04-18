using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace CourseAPI.Models {
    public class TimesheetRow {
        public int CourseId { get; set; }
        public int TaskId { get; set; }
        public List<double> Times { get; set; }

        public double TotalHours => Times.Sum();

        public TimesheetRow(int courseId, int taskId, List<double> times) {
            CourseId = courseId;
            TaskId = taskId;
            Times = times;
        }
    }
}