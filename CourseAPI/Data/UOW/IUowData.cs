using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;

namespace CourseAPI.Data.UOW
{
    public interface IUowData
    {
        IDeletableEntityRepository<Course> Course { get; }
        IDeletableEntityRepository<Storyboard> Storyboard { get; }
        IDeletableEntityRepository<Library> Libraries { get; }
        IDeletableEntityRepository<CourseUsers> CourseUsers { get; }
        IDeletableEntityRepository<Timesheet> Timesheets { get; }
        IDeletableEntityRepository<TimesheetTask> TimesheetTasks { get; }

        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
