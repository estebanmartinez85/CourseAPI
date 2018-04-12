using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Courses
{
    public class ScheduleWriterMeetingResponse : CourseEntity
    {
        public ScheduleWriterMeetingResponse(Course course ) : base(course) {
            WithScheduleWriterMeeting();
        }
    }
}
