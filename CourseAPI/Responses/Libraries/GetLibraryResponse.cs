using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Responses.Libraries
{
    public class GetLibraryResponse : LibraryEntity
    {
        public GetLibraryResponse(Controller controller, Library library) : base(controller, library)
        {
 
        }
    }
}
