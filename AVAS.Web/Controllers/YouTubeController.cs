using AVAS.Core.Domain;
using AVAS.Core.Services;
using AVAS.Web.ViewModels.YouTube;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AVAS.Web.Controllers
{
    public class YouTubeController : Controller
    {

        private IYoutubeSearchService _searchService;
        public YouTubeController(IYoutubeSearchService service)
        {
            _searchService = service;
        }

        public IActionResult Index()
        {
            return View(IndexViewModel.From());
        }

        [HttpPost]
        [Route("", Name = "IndexPost")]
        public async Task<IActionResult> IndexPost(string searchTerm, VideoRequestType type)
        {
            var videoResults = await _searchService.SearchVideosAsync(searchTerm, type, 1);
            var vm = IndexViewModel.From(searchTerm, type, videoResults);

            return View("Index", vm);
        }

        [HttpPost]
        public ActionResult AddVideoToQueue(VideoViewModel vm)
        {
            return RedirectToAction("CreateFromVideo", "Videos", vm);
        }
    }
}
