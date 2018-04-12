using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Libraries
{
    public class GetLibraryResponse : LibraryEntity
    {
        public GetLibraryResponse(Library library) : base(library) {
        }
    }
}
