using System.ComponentModel.DataAnnotations;

namespace CourseAPI.DTO.Course {
    public class ScheduleWriterMeetingDTO {
        [Required]
        public string Date { get; set; }
    }
}