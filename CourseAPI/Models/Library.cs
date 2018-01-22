using CourseAPI.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models
{
    public class Library : BaseModel
    {
        public int LibraryId { get; set; }
        public string Title { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
