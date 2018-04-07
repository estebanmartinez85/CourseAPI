using CourseAPI.Data.Common.Models;

namespace CourseAPI.Models
{
    public class CourseUsers : BaseModel
    {
        public int CourseUsersId { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
