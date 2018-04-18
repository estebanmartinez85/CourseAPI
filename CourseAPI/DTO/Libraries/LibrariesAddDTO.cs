using System.ComponentModel.DataAnnotations;

namespace CourseAPI.DTO.Libraries
{
    public class LibrariesAddDTO
    {
        [Required]
        public string Title { get; set; }
    }
}
