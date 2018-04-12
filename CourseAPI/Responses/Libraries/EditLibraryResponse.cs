using CourseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Responses.Libraries {
    public class EditLibraryResponse : BaseSirenEntity {
        public EditLibraryResponse(Library library) {
            this.WithSubEntity(new LibraryEntity(library)
                .WithDeleteLibrary());
        }
    }
}