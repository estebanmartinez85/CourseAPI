using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.DTO.Course
{
    public class CoursesAssignDTO
    {
        [Required]
        public string UserId { get; set; }
    }
}
