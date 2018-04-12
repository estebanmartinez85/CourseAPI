using CourseAPI.Models;
using CourseAPI.Responses.Storyboard;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Responses.Courses {
    public class InStoryboardResponse : CourseEntity {
        public InStoryboardResponse(Course course): base(course) {
            this.WithSubEntity(new StoryboardResponse(course.Storyboard));
        }
    }
}