using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Courses
{
    public class AssignWriterResponse : BaseSirenEntity
    {
        public AssignWriterResponse(Controller controller, Course course) : base(controller) {
            this.WithSubEntity(new CourseEntity(controller, course)
                                    .WithAssignWriter());
        }
    }
}
