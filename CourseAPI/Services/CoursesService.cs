using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Services
{
    public class CoursesService
    {
        private readonly IRepository<Course> _courses;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CoursesService(IRepository<Course> courses, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _courses = courses;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<Course> GetAllCourses()
        {
            return _courses.All().ToList();
        }

        public Course GetCourse(int id)
        {
            return _courses
                       .All()
                       .Include(c => c.Storyboard)
                       .Include(c => c.CourseUsers).ThenInclude(u => u.User)
                       .Single(c => c.CourseId == id) 
                            ?? throw new ArgumentNullException("No course with that id found.");
        }

        public List<string> GetAssignedUsers(int id) {
            return (from c in _courses.All()
                    from cu in c.CourseUsers
                    where c.CourseId == id
                    select cu.User.FullName).ToList();
        }

        public async Task<Course> UpdateBasic(int id, string code, string title, int libraryId)
        {
            Course course = await _courses.GetByIdAsync(id);
            if (course == null) throw new ArgumentNullException();
            
            course.Title = title;
            course.Code = code;

            _courses.Update(course);
            await _courses.SaveChangesAsync();
            return course;
        }

        public async Task<bool> AssignUser(int id, string userId)
        {
            Course course = GetCourse(id);
            course.CourseUsers.Add(new CourseUsers { CourseId = course.CourseId, UserId = userId });
            _courses.Update(course);
            await _courses.SaveChangesAsync();

            return true;
        }

        public List<Course> GetAssigned(string userId)
        {
            List<Course> courses = _courses
                                    .All()
                                    .Include(c => c.CourseUsers)
                                    .Include(c => c.Storyboard)
                                    .Where(c => c.CourseUsers
                                    .Any(cu => cu.UserId == userId)).ToList();
            return courses;
        }

        public async Task<Course> Create(string code, string title, int library)
        {
            Course course = new Course(code, title, library);
            _courses.Add(course);
            await _courses.SaveChangesAsync();

            return course;
        }

        public async Task<Course> AssignWriter(int id, string writerId) {
            Course course = GetCourse(id);

            if (course.Status != CourseStatus.AssignWriter) throw new InvalidOperationException("Course in wrong state.");

            ApplicationUser user = await _userManager.FindByIdAsync(writerId);

            if (!await _userManager.IsInRoleAsync(user, "Writer")) throw new ArgumentException("Not a Writer");

            course.CourseUsers.Add(new CourseUsers { CourseId = course.CourseId, UserId = user.Id });
            course.Status = CourseStatus.ScheduleWriterMeeting;

            _courses.Update(course);
            await _courses.SaveChangesAsync();

            return course;
        }

        public async Task<Course> ScheduleWriterMeeting(int id, string dateTime) {
            Course course = GetCourse(id);
            if (course.Status != CourseStatus.ScheduleWriterMeeting) throw new InvalidOperationException();

            course.WriterMeetingDate = dateTime;
            course.Status = CourseStatus.WriterMeetingWaiting;

            _courses.Update(course);
            await _courses.SaveChangesAsync();

            return course;
        }

        public async Task<Course> WriterMeetingWaiting(int id) {
            Course course = GetCourse(id);
            if (course.Status != CourseStatus.WriterMeetingWaiting) throw new InvalidOperationException();

            course.Status = CourseStatus.Storyboard;
            course.WriterMeetingCompleted = true;

            _courses.Update(course);
            await _courses.SaveChangesAsync();

            return course;
        }

        public async Task<Course> StoryboardReadyForReview(int id) {
            Course course = GetCourse(id);
            if(course.Status != CourseStatus.Storyboard) throw new InvalidOperationException();

            course.Status = CourseStatus.StoryboardComplete;
            course.StoryboardComplete = true;

            _courses.Update(course);
            await _courses.SaveChangesAsync();

            return course;
        }

        public async Task<bool> Delete(int id) {
            Course course = GetCourse(id);
            _courses.Delete(course);
            await _courses.SaveChangesAsync();
            return true;
        }


    }
}
