﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.DTO.Account
{
    public class AccountsRoleDTO
    {
        [Required]
        public string RoleName { get; set; }
    }
}
