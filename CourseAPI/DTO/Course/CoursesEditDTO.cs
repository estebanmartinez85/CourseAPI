using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.DTO.Course
{
    public class CoursesEditDTO
    {
        [Required, MaxLength(128)]
        public string Code { get; set; }
        [Required, MaxLength(256)]
        public string Title { get; set; }
        public string Comment { get; set; }
        [Display(Name = "Graphic Hours Goal")]
        public int GraphicHoursGoal { get; set; }
        [Display(Name = "Graphic Hours Actual")]
        public int GraphicHoursActual { get; set; }
        [Display(Name = "Storyboard Hours Goal")]
        public int StoryboardHoursGoal { get; set; }
        [Display(Name = "Storyboard Hours Actual")]
        public int StoryboardHoursActual { get; set; }

        [Required]
        public string LibraryId { get; set; }
    }
}
