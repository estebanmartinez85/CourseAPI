using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Courses
{
    public class WriterMeetingCompleteResponse : BaseSirenEntity
    {
        public WriterMeetingCompleteResponse(Controller controller, Course course)  : base(controller) {
            this.WithSubEntity(new CourseEntity(controller, course)
                .WithWriterMeetingComplete());
        }  
    }
}
