using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using CourseAPI.Data.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CourseAPI.Models {
    public class Timesheet : BaseModel {
        public Guid Id { get; private set; }  
        [JsonIgnore]
        public ApplicationUser User { get; private set; }
        public DateTime BeginDate { get; private set; }
        public DateTime EndDate { get; private set; }
        
        private string _rows;
        [NotMapped]
        public List<TimesheetRow> Rows {
            get => JsonConvert.DeserializeObject<List<TimesheetRow>>(string.IsNullOrEmpty(_rows)
                ? "[{}]"
                : _rows);
            set => _rows = JsonConvert.SerializeObject(value);
        }
        
        protected Timesheet() {}

        public Timesheet(ApplicationUser user) {
            Id = Guid.NewGuid();
            User = user;
            BeginDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            EndDate = BeginDate.AddDays(6);
            Rows = new List<TimesheetRow>();
        }

        public void AddRow(int courseId, int taskId, List<double> times) {
            List<TimesheetRow> rows = Rows;
            rows.Add(new TimesheetRow(courseId, taskId, times));
            
            Rows = rows;
        }
    }
}