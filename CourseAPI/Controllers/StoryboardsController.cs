using System.Threading.Tasks;
using CourseAPI.DTO.Storyboard;
using CourseAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;
using CourseAPI.Responses.Storyboard;
using CourseAPI.Services;

namespace CourseAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrator,Writer,Artist,Narrator")]
    public class StoryboardsController : Controller
    {
        private readonly StoryboardServices _services;

        public StoryboardsController(StoryboardServices services) {
            _services = services;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get([FromRoute] int id) {
            Storyboard sb = await _services.GetStoryboard(id);
            StoryboardResponse response = new StoryboardResponse(sb);
            return Ok(response.EntityToJson());
        }

        [HttpPatch("{id}/document")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveDocument([FromRoute] int id, [FromBody] StoryboardDTO model) {
            if (!ModelState.IsValid) return new BadRequestResult(); 
            
            Storyboard sb = await _services.UpdateStoryboardDocument(id, model.Document);
            
            return Ok();
        }


        [HttpPost("{id}")]
        public IActionResult Post([FromRoute] int id)
        {
            return new NotFoundResult();
        }

    }
}
