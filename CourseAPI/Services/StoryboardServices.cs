using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;

namespace CourseAPI.Services {
    public class StoryboardServices {
        private IRepository<Storyboard> _storyboards;

        public StoryboardServices(IRepository<Storyboard> storyboards) {
            _storyboards = storyboards;
        }

        public async Task<bool> Create() {
            Storyboard storyboard = new Storyboard();
            _storyboards.Add(storyboard);
            int res = await _storyboards.SaveChangesAsync();
            return res > 0;
        }
    }
}