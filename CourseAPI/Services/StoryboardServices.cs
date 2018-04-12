using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CourseAPI.Data.Common.Repos;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CourseAPI.Services {
    public class StoryboardServices {
        private readonly IRepository<Storyboard> _storyboards;

        public StoryboardServices(IRepository<Storyboard> storyboards) {
            _storyboards = storyboards;
        }

        public async Task<bool> Create() {
            Storyboard storyboard = new Storyboard();
            _storyboards.Add(storyboard);
            int res = await _storyboards.SaveChangesAsync();
            return res > 0;
        }

        public async Task<Storyboard> GetStoryboard(int id) {
            Storyboard sb = await _storyboards.All().SingleOrDefaultAsync(s => s.StoryboardId == id);
            return sb;
        }

        public async Task<Storyboard> UpdateStoryboardDocument(int id, string modelDocument) {
            Storyboard sb = await GetStoryboard(id);
            sb.Document = WebUtility.HtmlEncode(modelDocument);

            _storyboards.Update(sb);
            await _storyboards.SaveChangesAsync();

            return sb;
        }
    }
}