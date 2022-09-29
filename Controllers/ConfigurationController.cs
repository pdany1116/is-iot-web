using System;
using System.Net;
using System.Threading.Tasks;
using IsIoTWeb.Models;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text;
using IsIoTWeb.Utils;

namespace IsIoTWeb.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    public class ConfigurationController : Controller
    {
        ISinkRepository _sinkRepository;

        public ConfigurationController(ISinkRepository sinkRepository)
        {
            _sinkRepository = sinkRepository;
        }

        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = "ADMINISTRATOR")]
        [HttpPost]
        public async Task<IActionResult> AddCollectorAsync(string id)
        {
            try
            {
                Sink sink = await _sinkRepository.Get(StaticVariables.SinkId);
                sink.Collectors.Add(id);
                await _sinkRepository.Update(sink);
            }
            catch (Exception)
            {
                return Json(new Error() { ErrorMessages = { "Failed to add collector!" } });
            }

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
