﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Responses.Courses;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;
using CourseAPI.Responses.Storyboard;

namespace CourseAPI.Extensions
{
    public static class CourseStatusExtension
    {
        public static EntityBuilder GetResponse(this CourseStatus status, Controller controller, Course course) {
            switch (status)
            {
                case CourseStatus.AssignWriter:
                    return new AssignWriterResponse(course);
                case CourseStatus.ScheduleWriterMeeting:
                    return new ScheduleWriterMeetingResponse(course);
                case CourseStatus.WriterMeetingWaiting:
                    return new CourseEntity(course)
                        .WithWriterMeetingWaiting();
                case CourseStatus.Storyboard:
                    return new InStoryboardResponse(course);
                case CourseStatus.StoryboardReview:
                    break;
                case CourseStatus.StoryboardComplete:
                    break;
                case CourseStatus.AssignArtist:
                    break;
                case CourseStatus.ArtistMeetingScheduled:
                    break;
                case CourseStatus.ArtistMeetingComplete:
                    break;
                case CourseStatus.Graphics:
                    break;
                case CourseStatus.PeerReview:
                    break;
                case CourseStatus.ManagerReview:
                    break;
                case CourseStatus.Revisions:
                    break;
                case CourseStatus.GraphicsComplete:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }

            return new EntityBuilder();
        }
    }
}
