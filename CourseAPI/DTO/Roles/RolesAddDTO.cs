using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.DTO.Roles
{
    public class RolesAddDTO
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
    }
}
