using CourseAPI.Data.Common.Repos;
using CourseAPI.DTO.Libraries;
using CourseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class LibrariesController : Controller
    {
        private IRepository<Library> _repository;

        public LibrariesController(IRepository<Library> repository)
        {
            _repository = repository;
        }

        [HttpGet]
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
                return Ok(library);
            }
            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute]int id)
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
