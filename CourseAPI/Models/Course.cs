using CourseAPI.Attributes;
using CourseAPI.Data.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models
{
    public class Course : BaseModel
    {
        public int CourseId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Status { get; set; } 
        public bool Archived { get; set; }
        public bool AudioIn { get; set; }

        public int GraphicHoursGoal { get; set; }
        public int GraphicHoursActual { get; set; }
        public int StoryboardHoursGoal { get; set; }
        public int StoryboardHoursActual { get; set; }

        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }

        public string Glossary { get; set; }

        public int Length { get; set; }

        public string Lessons { get; set; }
        public string NarrationTone { get; set; }
        public string Objective { get; set; }
        public string References { get; set; }

        public virtual ICollection<CourseUsers> CourseUsers { get; set; } = new List<CourseUsers>();

        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }
        public virtual Storyboard Storyboard { get; set; }
    }
}
