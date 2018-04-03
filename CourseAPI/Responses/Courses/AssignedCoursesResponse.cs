using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Courses
{
    public class AssignedCoursesResponse : EntityBuilder
    {
        public AssignedCoursesResponse(Controller controller, List<Course> courses) {
            this.WithClass("course")
                .WithClass("collection")
                .WithProperty("count", courses.Count);
            foreach (Course course in courses) {
                this.WithSubEntity(new CourseEntity(controller, course)
                    .WithDeleteCourse());
            }
        }
    }
}
