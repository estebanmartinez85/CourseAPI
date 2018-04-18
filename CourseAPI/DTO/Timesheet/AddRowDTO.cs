using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseAPI.DTO.Timesheet {
    public class AddRowDTO {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public List<double> Times { get; set; }
    }
}