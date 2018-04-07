using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Courses
{
    public class WriterMeetingCompleteResponse : CourseEntity
    {
        public WriterMeetingCompleteResponse(Controller controller, Course course)  : base(controller, course) {
                WithWriterMeetingWaiting();
        }  
    }
}
