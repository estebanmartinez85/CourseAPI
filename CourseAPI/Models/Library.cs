using System.Collections.Generic;
using CourseAPI.Data.Common.Models;

namespace CourseAPI.Models
{
    public class Library : BaseModel
    {
        public int LibraryId { get; private set; }
        public string Title { get; private set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        protected Library() { }
        public Library(string title)
        {
            Title = title;
        }

        public void SetTitle(string title) {
            Title = title;
        }
    }
}
