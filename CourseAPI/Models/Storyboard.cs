using CourseAPI.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models
{
    public class Storyboard : BaseModel
    {
        public int StoryboardId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Slide> Slides { get; set; }

        public bool Active { get; set; }
        public bool SlidesReadyToWork { get; set; }
        public bool SlidesGraphicsComplete { get; set; }
        public bool SlidesNarrationComplete { get; set; }
        public bool Complete { get; set; }

    }
}
