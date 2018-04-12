using CourseAPI.Responses.Courses;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Roles
{
    public class RoleEntity : BaseSirenEntity {
        private readonly ApplicationRole _role;
        public RoleEntity(ApplicationRole role) {
            _role = role;
            this.WithClass("role")
                .WithProperty("id", role.Id)
                .WithProperty("name", role.Name)
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(GetBaseURL() + "roles/" + role.Id));
        }

        public RoleEntity WithAddRole()
        {
             this.WithAction( new ActionBuilder()
                    .WithName("add-role")
                    .WithTitle("Add Role")
                    .WithType("application/json")
                    .WithMethod("POST")
                    .WithHref(GetBaseURL() + "roles/")
                    .WithField(new FieldBuilder()
                        .WithName("title")
                        .WithType("text")));
            return this;
        }

        public RoleEntity WithEditRole()
        {
            this.WithAction(
                new ActionBuilder()
                    .WithName("edit-role")
                    .WithTitle("Edit Role")
                    .WithType("application/json")
                    .WithMethod("POST")
                    .WithHref(GetBaseURL() + "roles/" + "edit/" + _role.Id)
                    .WithField(new FieldBuilder()
                        .WithName("title")
                        .WithType("text")));
            return this;
        }

        public RoleEntity WithDeleteRole()
        {
            this.WithAction(
                new ActionBuilder()
                    .WithName("delete-role")
                    .WithTitle("Delete Role")
                    .WithMethod("DELETE")
                    .WithHref(GetBaseURL() + "roles/" + _role.Id));
            return this;
        }
    }
}
