using CourseAPI.Data.Common.Repos;
using CourseAPI.Data.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;

namespace CourseAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class StoryboardsController : Controller
    {
        private readonly IUowData _data;

        public StoryboardsController(IRepository<Storyboard> storyboards)
        {
            
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {

            return new NotFoundResult();
        }
        
        [HttpPost("{id}")]
        public IActionResult Post([FromRoute] int id)
        {

            return new NotFoundResult();
        }

    }
}
