using System.Linq;
using CourseAPI.Data.Common.Models;

namespace CourseAPI.Data.Common.Repos
{
    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
