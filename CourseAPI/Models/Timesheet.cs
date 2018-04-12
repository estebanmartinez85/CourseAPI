using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CourseAPI.Models {
    public class Timesheet {
        public Guid Id { get; set; }
        public Course Course { get; set; }
        public TimesheetTask Task { get; set; }

        private string _week;
        [NotMapped]
        public Dictionary<DateTime, int> Week {
            get => JsonConvert.DeserializeObject<Dictionary<DateTime, int>>(string.IsNullOrEmpty(_week)
                ? "[{}]"
                : _week);
            set => _week = JsonConvert.SerializeObject(value);
        }
    }
}