using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.DTO.Course
{
    public class CoursesAddDTO
    {
        [Required, MaxLength(128)]
        public string Code { get; set; }
        [Required, MaxLength(256)]
        public string Title { get; set; }

        [Required]
        public int LibraryId { get; set; }
    }
}
