using CourseAPI.Responses.Courses;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Accounts
{
    public class AccountEntity : BaseSirenEntity {
        private readonly ApplicationUser _account;
        public AccountEntity(ApplicationUser account){
            _account = account;
            this.WithClass("account")
                .WithProperty("id", account.Id)
                .WithProperty("username", account.UserName)
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(GetBaseURL() + "accounts/" + account.Id));
        }

        public AccountEntity WithAddAccount()
        {
             this.WithAction( new ActionBuilder()
                    .WithName("add-account")
                    .WithTitle("Add Account")
                    .WithType("application/json")
                    .WithMethod("POST")
                    .WithHref(GetBaseURL() + "accounts/")
                    .WithField(new FieldBuilder()
                        .WithName("title")
                        .WithType("text")));
            return this;
        }

        public AccountEntity WithEditAccount()
        {
            this.WithAction(
                new ActionBuilder()
                    .WithName("edit-account")
                    .WithTitle("Edit Account")
                    .WithType("application/json")
                    .WithMethod("POST")
                    .WithHref(GetBaseURL() + "accounts/" + "edit/" + _account.Id)
                    .WithField(new FieldBuilder()
                        .WithName("title")
                        .WithType("text")));
            return this;
        }

        public AccountEntity WithDeleteAccount()
        {
            this.WithAction(
                new ActionBuilder()
                    .WithName("delete-account")
                    .WithTitle("Delete Account")
                    .WithMethod("DELETE")
                    .WithHref(GetBaseURL() + "accounts/" + _account.Id));
            return this;
        }
    }
}
