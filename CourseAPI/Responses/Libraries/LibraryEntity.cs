using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Responses.Courses;
using Courses.Models;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Responses.Libraries
{
    public class LibraryEntity : BaseSirenEntity {
        private Library _library;
        public LibraryEntity(Controller controller, Library library) : base(controller) {
            _library = library;
            this.WithClass("library")
                .WithProperty("id", _library.LibraryId)
                .WithProperty("title", _library.Title)
                .WithProperty("courseCount", _library.Courses.Count)
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(GetBaseURL() + "libraries/" + _library.LibraryId));
            foreach (Course course in _library.Courses) {
                this.WithSubEntity(new CourseEntity(controller, course));
            }
            
        }

        public LibraryEntity WithAddLibrary()
        {
             this.WithAction( new ActionBuilder()
                    .WithName("add-library")
                    .WithTitle("Add Library")
                    .WithType("application/json")
                    .WithMethod("POST")
                    .WithHref(GetBaseURL() + "libraries/")
                    .WithField(new FieldBuilder()
                        .WithName("title")
                        .WithType("text")));
            return this;
        }

        public LibraryEntity WithEditLibrary()
        {
            this.WithAction(
                new ActionBuilder()
                    .WithName("edit-library")
                    .WithTitle("Edit Library")
                    .WithType("application/json")
                    .WithMethod("POST")
                    .WithHref(GetBaseURL() + "libraries/" + "edit/" + _library.LibraryId)
                    .WithField(new FieldBuilder()
                        .WithName("title")
                        .WithType("text")));
            return this;
        }

        public LibraryEntity WithDeleteLibrary()
        {
            this.WithAction(
                new ActionBuilder()
                    .WithName("delete-library")
                    .WithTitle("Delete Library")
                    .WithMethod("DELETE")
                    .WithHref(GetBaseURL() + "libraries/" + _library.LibraryId));
            return this;
        }
    }
}
