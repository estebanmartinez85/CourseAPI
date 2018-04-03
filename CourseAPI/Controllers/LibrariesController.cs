using CourseAPI.DTO.Libraries;
using Courses.Services;
using Courses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using CourseAPI.Helpers;
using CourseAPI.Responses.Libraries;
using MoreLinq;

namespace CourseAPI.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class LibrariesController : Controller
    {
        private readonly LibrariesService _service;

        public LibrariesController(LibrariesService service)
        {
            _service = service;
        }

        [HttpGet(Name = "AllLibraries")]
        public IActionResult All()
        {
            List<Library> libraries = _service.GetAll();
            AllSirenResponse response = new AllSirenResponse(this, libraries);
            return Ok(response.EntityToJson());
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get([FromRoute] int id)
        {
            Library library = _service.GetById(id);

            if (library == null) return new BadRequestResult();
            GetLibraryResponse response = new GetLibraryResponse(this, library);
            return Ok(response.EntityToJson());
        }
        [HttpPost(Name = "AddLibrary")]
        public async Task<IActionResult> Add([FromBody] LibrariesAddDTO model)
        {
            if (!ModelState.IsValid) return new BadRequestResult();
            try
            {
                Library library = await _service.AddNewLibraryAsync(model.Title);
                return Ok();
            } catch
            {
                return StatusCode(422, new { error = "Duplicate Library Title" });
            }
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody] LibrariesEditDTO model)
        {
            if (!ModelState.IsValid) return new BadRequestResult();
            Library library = await _service.EditLibrary(id, model.Title);    

            return Ok();
        }
        [HttpDelete("{id}", Name = "DeleteLibrary")]
        public IActionResult Delete([FromRoute]int id)
        {
            try
            {
                _service.DeleteLibrary(id);
                return Ok();
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
