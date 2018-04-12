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
        public WriterMeetingCompleteResponse(Course course)  : base(course) {
                WithWriterMeetingWaiting();
        }  
    }
}
