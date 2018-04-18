using CourseAPI.DTO.Libraries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using CourseAPI.Helpers;
using CourseAPI.Responses.Libraries;
using CourseAPI.Services;
using CourseAPI.Models;

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
            AllSirenResponse response = new AllSirenResponse(libraries);
            return Ok(response.EntityToJson());
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get([FromRoute] int id)
        {
            Library library = _service.GetById(id);

            if (library == null) return new BadRequestResult();
            GetLibraryResponse response = new GetLibraryResponse(library);
            return Ok(response.EntityToJson());
        }
        [HttpPost(Name = "AddLibrary")]
        public async Task<IActionResult> Add([FromBody] LibrariesAddDTO model)
        {
            if (!ModelState.IsValid) return new BadRequestResult();
            try
            {
                Library library = await _service.AddNewLibraryAsync(model.Title);
                AddLibraryResponse response = new AddLibraryResponse(library);
                return Ok(response.EntityToJson());
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
            EditLibraryResponse response = new EditLibraryResponse(library);
            return Ok(response.EntityToJson());
        }
        [HttpDelete("{id}", Name = "DeleteLibrary")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                if (await _service.DeleteLibrary(id)) {
                    return Ok();
                }
                else {
                    return new BadRequestResult();
                }
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
