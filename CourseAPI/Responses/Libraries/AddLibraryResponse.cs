using CourseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Responses.Libraries {
    public class AddLibraryResponse : BaseSirenEntity {
        public AddLibraryResponse(Controller controller, Library library) : base(controller)
        {
            this.WithSubEntity(new LibraryEntity(controller, library)
                .WithEditLibrary()
                .WithDeleteLibrary());
        }
    }
}