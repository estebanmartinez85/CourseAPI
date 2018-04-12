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
        public AllSirenResponse(List<Library> libraries)
        {
            this.WithClass("library")
                .WithClass("collection")
                .WithProperty("count", libraries.Count);
            foreach (Library library in libraries) {
                this.WithSubEntity(new LibraryEntity(library)
                                            .WithEditLibrary()
                                            .WithDeleteLibrary());
            }
                this.WithAction( new ActionBuilder()
                    .WithName("add-library")
                    .WithTitle("Add Library")
                    .WithType("application/json")
                    .WithMethod("POST")
                    .WithHref("http://localhost:5000/api/v1/libraries")
                    .WithField(new FieldBuilder()
                        .WithName("title")
                        .WithType("text")));
        }
    }
}
