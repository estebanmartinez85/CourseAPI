using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Courses
{
    public class StoryboardReadyForReviewResponse : BaseSirenEntity
    {
        public StoryboardReadyForReviewResponse(Course course) {
            this.WithSubEntity(new CourseEntity(course)
                .WithStoryboardReadyForReview());
        }
    }
}
