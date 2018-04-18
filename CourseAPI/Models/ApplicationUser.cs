using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CourseAPI.Data.Common.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace CourseAPI.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [JsonProperty]
        public Timesheet CurrentTimesheet { get; set; }
        
        [JsonProperty]
        public string FirstName { get; set; }
        
        [JsonProperty]
        public string LastName { get; set; }

        [JsonIgnore]
        public virtual ICollection<CourseUsers> CourseUsers { get; set; } = new List<CourseUsers>();

        [NotMapped]
        [JsonProperty]
        public string FullName => FirstName + " " + LastName;
    }
}
