using System.Collections.Generic;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Accounts
{
    public class AccountsResponse : EntityBuilder
    {
        public AccountsResponse(Controller controller, IReadOnlyCollection<ApplicationUser> users)
        {
            this.WithClass("accounts")
                .WithClass("collection")
                .WithProperty("count", users.Count);
            foreach (ApplicationUser user in users) {
                this.WithSubEntity(new AccountEntity(controller, user));
            }
        }
    }
}
