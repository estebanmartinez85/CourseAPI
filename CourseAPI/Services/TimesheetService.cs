using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Services {
    public class TimesheetService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Timesheet> _timesheets;
        private IRepository<ApplicationUser> _users;

        public TimesheetService(
            IRepository<Timesheet> timesheets, 
            UserManager<ApplicationUser> usermanager,
            IRepository<ApplicationUser> users) {
            _timesheets = timesheets;
            _userManager = usermanager;
            _users = users;
        }

        public async Task<Timesheet> GetCurrentTimesheet(string userId) {
//            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            ApplicationUser user = await _users.All().Include("CurrentTimesheet").SingleAsync(u => u.Id == userId);
            if (user.CurrentTimesheet != null &&
                (user.CurrentTimesheet.BeginDate.Date <= DateTime.Today.Date 
                 && user.CurrentTimesheet.EndDate.Date >= DateTime.Today.Date)) {
                return user.CurrentTimesheet;
            }

            Timesheet timesheet = new Timesheet(user);
            user.CurrentTimesheet = timesheet;
            await _userManager.UpdateAsync(user);
            return timesheet;
        }

        public async Task<Timesheet> ClearRows(Guid id) {
            Timesheet timesheet = _timesheets.All().Single(t => t.Id == id) ?? throw new ArgumentNullException();
            List<TimesheetRow> rows = timesheet.Rows;
            rows.Clear();
            timesheet.Rows = rows;
            _timesheets.Update(timesheet);
            await _timesheets.SaveChangesAsync();
            return timesheet;
        }
        public async Task<Timesheet> AddRow(Timesheet ts, int courseId, int taskId, List<double> times) {
            //Timesheet ts = _timesheets.All().Single(t => t.Id == timesheetId) ?? throw new ArgumentNullException();
            ts.AddRow(courseId, taskId, times);
            _timesheets.Update(ts);
            await _timesheets.SaveChangesAsync();
            return ts;
        }
    }
}