﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.DTO.Libraries
{
    public class LibrariesEditDTO
    {
        [Required]
        public string Title { get; set; }
    }
}
