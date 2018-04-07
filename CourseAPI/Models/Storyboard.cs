using System;
using System.ComponentModel.DataAnnotations.Schema;
using CourseAPI.Data.Common.Models;

namespace CourseAPI.Models
{
    public class Storyboard : BaseModel
    {
        public int StoryboardId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public SlideCollection Slides { get; set; }
        
        public bool Active { get; set; }
        
        public bool SlidesGraphicsComplete { get; set; }
        public bool SlidesNarrationComplete { get; set; }
        
        public bool ReadyForReview { get; set; }
        public bool Complete { get; set; }

        public void AddSlide(Slide slide) {
            Slides.Add(slide);
        }
    }
}