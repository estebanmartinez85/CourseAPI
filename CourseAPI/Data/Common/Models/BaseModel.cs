using System;

namespace CourseAPI.Data.Common.Models
{
    public abstract class BaseModel : IAuditInfo, IDeletableEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public BaseModel()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
