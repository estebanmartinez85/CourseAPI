using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Libraries
{
    public class AllSirenResponse : EntityBuilder
    {
        public AllSirenResponse(Controller controller, List<Library> libraries)
        {
            this.WithClass("library")
                .WithClass("collection")
                .WithProperty("count", libraries.Count);
            foreach (Library library in libraries) {
                this.WithSubEntity(new LibraryEntity(controller, library)
                                            .WithEditLibrary()
                                            .WithDeleteLibrary());
            }
                this.WithAction( new ActionBuilder()
                    .WithName("add-library")
                    .WithTitle("Add Library")
                    .WithType("application/json")
                    .WithMethod("POST")
                    .WithHref($"{controller.Request.Scheme}://{controller.Request.Host}{controller.Request.Path}")
                    .WithField(new FieldBuilder()
                        .WithName("title")
                        .WithType("text")));
        }
    }
}
