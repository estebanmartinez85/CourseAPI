using CourseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Responses.Libraries {
    public class AddLibraryResponse : BaseSirenEntity {
        public AddLibraryResponse(Library library)
        {
            this.WithSubEntity(new LibraryEntity(library)
                .WithEditLibrary()
                .WithDeleteLibrary());
        }
    }
}