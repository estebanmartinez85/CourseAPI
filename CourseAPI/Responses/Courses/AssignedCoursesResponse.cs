using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Courses
{
    public class AssignedCoursesResponse : EntityBuilder, ISubEntityBuilder
    {
        public AssignedCoursesResponse(List<Course> courses) {
            this.WithClass("course")
                .WithClass("collection")
                .WithProperty("count", courses.Count);
            foreach (Course course in courses) {
                this.WithSubEntity(new CourseEntity(course)
                    .WithDeleteCourse());
            }
            WithAction(new ActionBuilder()
                .WithName("add-course")
                .WithTitle("Add Course")
                .WithType("application/json")
                .WithMethod("POST")
                .WithHref($"http://localhost:5000/courses/")
                .WithField(new FieldBuilder()
                    .WithName("title")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("libraryid")
                    .WithType("text"))
                .WithField(new FieldBuilder()
                    .WithName("code")
                    .WithType("text")));
            
        }
    }
}
