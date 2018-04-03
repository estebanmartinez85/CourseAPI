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
                                            .WithAddLibrary()
                                            .WithEditLibrary()
                                            .WithDeleteLibrary());
            }
        }
    }
}
