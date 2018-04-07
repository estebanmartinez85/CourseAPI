using CourseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Responses.Libraries {
    public class EditLibraryResponse : BaseSirenEntity {
        public EditLibraryResponse(Controller controller, Library library) : base(controller) {
            this.WithSubEntity(new LibraryEntity(controller, library)
                .WithDeleteLibrary());
        }
    }
}