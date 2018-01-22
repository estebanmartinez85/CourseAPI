using CourseAPI.Data.Common.Models;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Data.Repos;
using CourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Data.UOW
{
    public class UowData : IUowData
    {
        private readonly ApplicationDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();


        public IDeletableEntityRepository<Course> Course {
            get { return this.GetRepository<Course>(); }
        }
        public IDeletableEntityRepository<Storyboard> Storyboard {
            get { return this.GetRepository<Storyboard>(); }
        }
        public IDeletableEntityRepository<Library> Libraries {
            get { return this.GetRepository<Library>(); }
        }
        public IDeletableEntityRepository<CourseUsers> CourseUsers {
            get { return this.GetRepository<CourseUsers>(); }
        }
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
