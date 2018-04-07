using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Courses
{
    public class CourseEntity : BaseSirenEntity {
        private Course _course;
        public CourseEntity(Controller controller, Course course) : base(controller) {
            _course = course;
            this.WithClass("course")
                .WithProperty("id", _course.CourseId)
                .WithProperty("code", course.Code)
                .WithProperty("title", course.Title)
                .WithProperty("status", course.Status.ToString())
                .WithLink(
                    new LinkBuilder()
                        .WithRel("self")
                        .WithHref(GetBaseURL() + "courses/"  + _course.CourseId));
        }

        public CourseEntity WithAddCourse()
        {
            this.WithAction(new ActionBuilder()
                .WithName("add-course")
                .WithTitle("Add Course")
                .WithType("application/json")
                .WithMethod("POST")
                .WithHref(GetBaseURL() + "courses/")
                .WithField(new FieldBuilder()
                    .WithName("title")
                    .WithType("string"))
                .WithField(new FieldBuilder()
                    .WithName("code")
                    .WithType("string")));
            return this;
        }

        public CourseEntity WithAssignWriter() {
            this.WithAction(new ActionBuilder()
                .WithName("assign-writer")
                .WithTitle("Assign Writer")
                .WithType("application/json")
                .WithMethod("PATCH")
                .WithHref(GetBaseURL() + "courses/" + _course.CourseId + "/AssignWriter")
                .WithField(
                    new FieldBuilder()
                    .WithName("writerId")
                    .WithType("int")));
            return this;
        }

        public CourseEntity WithScheduleWriterMeeting()
        {
            this.WithAction(new ActionBuilder()
                .WithName("schedule-writer-meeting")
                .WithTitle("Schedule Writer Meeting")
                .WithType("application/json")
                .WithMethod("PATCH")
                .WithHref(GetBaseURL() + "courses/" + _course.CourseId + "/ScheduleWriterMeeting")
                .WithField(
                    new FieldBuilder()
                        .WithName("date")
                        .WithType("string")));
            return this;
        }

        public CourseEntity WithWriterMeetingWaiting()
        {
            this.WithAction(new ActionBuilder()
                .WithName("writer-meeting-waiting")
                .WithTitle("Writer Meeting Waiting")
                .WithType("application/json")
                .WithMethod("PATCH")
                .WithHref(GetBaseURL() + "courses/" + _course.CourseId + "/WriterMeetingWaiting")
                .WithField(new FieldBuilder()
                    .WithName("complete")
                    .WithType("bool")));
            return this;
        }

        public CourseEntity WithStoryboardReadyForReview()
        {
            this.WithAction(new ActionBuilder()
                .WithName("storyboard-complete")
                .WithTitle("Set Storyboard Complete")
                .WithType("application/json")
                .WithMethod("PATCH")
                .WithHref(GetBaseURL() + "courses/" + _course.CourseId + "/StoryboardReadyForReview"));
            return this;
        }

        public CourseEntity WithDeleteCourse()
        {
            this.WithAction(
                new ActionBuilder()
                    .WithName("delete-course")
                    .WithTitle("Delete Course")
                    .WithMethod("DELETE")
                    .WithHref(GetBaseURL() + "courses/" + _course.CourseId));
            return this;
        }
    }
}
