using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseAPI.Data.Common.Models;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Data.Repos;
using CourseAPI.Models;

namespace CourseAPI.Data.UOW
{
    public class UowData : IUowData
    {
        private readonly ApplicationDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();


        public IDeletableEntityRepository<Course> Course => this.GetRepository<Course>();
        public IDeletableEntityRepository<Storyboard> Storyboard => this.GetRepository<Storyboard>();
        public IDeletableEntityRepository<Library> Libraries => this.GetRepository<Library>();
        public IDeletableEntityRepository<CourseUsers> CourseUsers => this.GetRepository<CourseUsers>();
        public IDeletableEntityRepository<Timesheet> Timesheets => this.GetRepository<Timesheet>();
        public IDeletableEntityRepository<TimesheetTask> TimesheetTasks => this.GetRepository<TimesheetTask>();

        public UowData(ApplicationDbContext context)
        {
            this.context = context;
        }

        private IDeletableEntityRepository<T> GetRepository<T>() where T : BaseModel
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfDeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }
            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }



        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
