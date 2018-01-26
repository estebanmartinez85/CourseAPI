using CourseAPI.DTO.Libraries;
using Courses.Data.Common.Repos;
using Courses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class LibrariesController : Controller
    {
        private IRepository<Library> _repository;

        public LibrariesController(IRepository<Library> repository)
        {
            _repository = repository;
        }

        [HttpGet("All")]
        public IActionResult All()
        {
            var libraries = _repository
                .All()
                .Select(
                l => new {
                    l.LibraryId,
                    l.Title
                }).ToList();
            return Ok(libraries);
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get([FromRoute] int id)
        {
            Library library = _repository.All().Single(l => l.LibraryId == id);
            if(library != null)
            {
                return Ok(library);
            }
            return new BadRequestResult();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LibrariesAddDTO model)
        {
            if (ModelState.IsValid)
            {
                Library library = new Library
                {
                    Title = model.Title
                };
                _repository.Add(library);
                await _repository.SaveChangesAsync();
                
                return CreatedAtAction("Get", new { id = library.LibraryId }, null);
            }
            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                Library library = _repository.All().Single(l => l.LibraryId == id);
                _repository.Delete(library);
                await _repository.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
