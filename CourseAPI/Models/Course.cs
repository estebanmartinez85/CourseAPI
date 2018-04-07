using System;
using System.Collections.Generic;
using CourseAPI.Data.Common.Models;
using Newtonsoft.Json;

namespace CourseAPI.Models
{
    public enum CourseStatus {
        AssignWriter,
        ScheduleWriterMeeting,
        WriterMeetingWaiting,
        Storyboard,
        StoryboardReview,
        StoryboardComplete,
        AssignArtist,
        ArtistMeetingScheduled,
        ArtistMeetingComplete,
        Graphics,
        PeerReview,
        ManagerReview,
        Revisions,
        GraphicsComplete
    }

    public class Course : BaseModel
    {
        public int CourseId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public CourseStatus Status { get; set; } 
        public bool Archived { get; set; }
        public bool AudioIn { get; set; }

        public string WriterMeetingDate { get; set; }
        public bool WriterMeetingCompleted { get; set; }

        public int GraphicHoursGoal { get; set; }
        public int GraphicHoursActual { get; set; }
        public int StoryboardHoursGoal { get; set; }
        public int StoryboardHoursActual { get; set; }
        public bool StoryboardComplete { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }

        public string Glossary { get; set; }

        public int Length { get; set; }

        public string Lessons { get; set; }
        public string NarrationTone { get; set; }
        public string Objective { get; set; }
        public string References { get; set; }

        public virtual ICollection<CourseUsers> CourseUsers { get; set; } = new List<CourseUsers>();
        public virtual Storyboard Storyboard { get; private set; } = new Storyboard();

        public int LibraryId { get; set; }
        [JsonIgnore]
        public Library Library { get; set; }

        protected Course(){}
        public Course(string code, string title, int libraryId)
        {
            Code = code;
            Title = title;
            LibraryId = libraryId;
            Status = CourseStatus.AssignWriter;
            //CourseUsers = new List<CourseUsers>();
        }
    }
}
