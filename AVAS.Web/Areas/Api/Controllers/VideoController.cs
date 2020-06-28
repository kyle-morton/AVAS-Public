using AVAS.Core.Services;
using AVAS.Web.Areas.Api.Models;
using AVAS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AVAS.Web.Areas.Api.Controllers
{
    [Route("api/videos")]
    public class VideosController : Controller
    {

        private readonly ILogger<VideosController> _logger;
        private readonly IVideoService _service;

        public VideosController(ILogger<VideosController> logger, IVideoService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("Pop")]
        public async Task<IActionResult> PopRequest()
        {
            var request = await _service.GetNextPendingRequestAsync();
            return Json(request);
        }

        [HttpPost]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateRequest([FromBody]UpdateRequestStatusModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.GetErrors() });
            }

            await _service.UpdateRequestStatusAsync(model.Id, model.Status);

            return Json(new { success = true });
        }

    }
}