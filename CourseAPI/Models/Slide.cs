using CourseAPI.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models
{
    public class Slide : BaseModel
    {
        public int SlideId { get; set; }
        public string Audio { get; set; }
        public bool Complete { get; set; }
        public string File { get; set; }
        public string GraphicNote { get; set; }
        public string WriterNote { get; set; }
        public int Number { get; set; }
        public int StoryboardId { get; set; }
        public Storyboard Storyboard { get; set; }
        public string Text { get; set; }
    }
}
