using AVAS.Core.Models.Filters;
using AVAS.Core.Services;
using AVAS.Web.ViewModels.Videos;
using AVAS.Web.ViewModels.YouTube;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AVAS.Web.Controllers
{
    public class VideosController : Controller
    {

        private readonly ILogger<VideosController> _logger;
        private readonly IVideoService _service;

        public VideosController(ILogger<VideosController> logger, IVideoService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _service.GetRequestsAsync(new VideoRequestFilter());

            return View(requests.Select(RequestListItemViewModel.From).ToList());
        }

        public IActionResult Create()
        {
            return View(CreateRequestViewModel.From());
        }

        public IActionResult CreateFromVideo(VideoViewModel vm)
        {
            return View("Create", CreateRequestViewModel.From(vm.Url));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreateRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _service.CreateRequestAsync(model.ToModel());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                await _service.UpdateRequestStatusAsync(id, Core.Domain.VideoRequestStatus.Removed);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Requeue/{id}")]
        public async Task<IActionResult> Requeue(int id)
        {
            if (id > 0)
            {
                await _service.UpdateRequestStatusAsync(id, Core.Domain.VideoRequestStatus.Pending);
            }

            return RedirectToAction("Index");
        }
    }
}
