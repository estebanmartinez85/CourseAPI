using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace CourseAPI.Models
{
    [NotMapped]
    public class Slide
    {
        public Guid Id { get; set; }
        public string Audio { get; set; }
        public bool Complete { get; set; }
        public string File { get; set; }
        public string GraphicNote { get; set; }
        public string WriterNote { get; set; }
        public string Text { get; set; }

        public Slide(string graphicNote, string writerNote, string text) {
            this.Id = Guid.NewGuid();
            this.GraphicNote = graphicNote;
            this.WriterNote = writerNote;
            this.Text = text;
        }
    }
}
