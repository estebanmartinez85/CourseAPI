﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Responses.Courses
{
    public class StoryboardReadyForReviewResponse : BaseSirenEntity
    {
        public StoryboardReadyForReviewResponse(Controller controller, Course course) : base(controller) {
            this.WithSubEntity(new CourseEntity(controller, course)
                .WithStoryboardReadyForReview());
        }
    }
}
