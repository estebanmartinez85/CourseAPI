using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Libraries
{
    public class GetLibraryResponse : BaseSirenEntity
    {
        public GetLibraryResponse(Controller controller, Library library) : base(controller)
        {
            this.WithSubEntity(new LibraryEntity(controller, library)
                .WithEditLibrary()
                .WithDeleteLibrary());
        }
    }
}
