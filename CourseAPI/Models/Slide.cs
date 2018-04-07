using System.ComponentModel.DataAnnotations.Schema;

namespace CourseAPI.Models
{
    [NotMapped]
    public class Slide
    {
        public string Audio { get; set; }
        public bool Complete { get; set; }
        public string File { get; set; }
        public string GraphicNote { get; set; }
        public string WriterNote { get; set; }
        public int Number { get; set; }
        public string Text { get; set; }
    }
}
