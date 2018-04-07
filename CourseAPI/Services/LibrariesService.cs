using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Services
{
    public class LibrariesService
    {
        private readonly IRepository<Library> _libraries;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public LibrariesService(IRepository<Library> libraries, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _libraries = libraries;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<Library> GetAll()
        {
            List<Library> libraries = _libraries.All().Include("Courses").ToList();
            return libraries;
        }

        public Library GetById(int id)
        {
            Library library = _libraries.All().Include(l => l.Courses).SingleOrDefault(l => l.LibraryId == id);
            return library;
        }

        public async Task<Library> AddNewLibraryAsync(string title)
        {
            Library library = new Library(title);
            _libraries.Add(library);
            await _libraries.SaveChangesAsync();

            return library;
        }

        public async Task<bool> DeleteLibrary(int id)
        {
            Library library = _libraries.All().SingleOrDefault(l => l.LibraryId == id);
            if (library != null)
            {
                _libraries.Delete(library);
                await _libraries.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException();
        }

        public async Task<Library> EditLibrary(int id, string title)
        {
            Library library = _libraries.All().SingleOrDefault(l => l.LibraryId == id);
            if (library != null) {
                library.SetTitle(title);
                _libraries.Update(library);
                await _libraries.SaveChangesAsync();
                return library;
            }
            throw new KeyNotFoundException();
        }
    }
}
