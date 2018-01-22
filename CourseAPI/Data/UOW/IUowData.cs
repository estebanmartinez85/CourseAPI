using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Data.UOW
{
    public interface IUowData
    {
        IDeletableEntityRepository<Course> Course { get; }
        IDeletableEntityRepository<Storyboard> Storyboard { get; }
        IDeletableEntityRepository<Library> Libraries { get; }
        IDeletableEntityRepository<CourseUsers> CourseUsers { get; }

        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
