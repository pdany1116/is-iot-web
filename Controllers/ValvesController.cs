using IsIoTWeb.Models;
using IsIoTWeb.Mqtt;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Controllers
{
    [Authorize]
    public class ValvesController : Controller
    {
        private IValveRepository _valveRepository;
        private IMqttClient _mqttClient;

        public ValvesController(IValveRepository valveRepository, IMqttClient mqttClient)
        {
            _valveRepository = valveRepository;
            _mqttClient = mqttClient;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Logs");
        }

        public async Task<IActionResult> Logs()
        {
            await _mqttClient.Connect();
            var result = await _valveRepository.GetAll();
            return View(result);
        }

        public async Task<IActionResult> Control()
        {
            await _mqttClient.Connect();
            await _mqttClient.Publish($"/valves/status/request", "");
            await _mqttClient.Subscribe("/valves/status/response");
            var message = _mqttClient.GetLastPayload();
            if (string.IsNullOrEmpty(message))
            {
                return View(new List<ValveState>());
            }
            List<ValveState> valves = JsonConvert.DeserializeObject<List<ValveState>>(message);
            return View(valves);
        }

        public IActionResult Configure()
        {
            return View();
        }

        public async Task<RedirectToActionResult> TurnOn(ValveState valve)
        {
            await _mqttClient.Connect();
            await _mqttClient.Publish($"/valves/{valve.ValveId}", "on");
            return RedirectToAction("Control");
        }

        public async Task<RedirectToActionResult> TurnOff(ValveState valve)
        {
            await _mqttClient.Connect();
            await _mqttClient.Publish($"/valves/{valve.ValveId}", "off");
            return RedirectToAction("Control");
        }

        [HttpPost]
        public JsonResult GetValvesState()
        {
            var message = _mqttClient.GetLastPayload();
            if (string.IsNullOrEmpty(message))
            {
                return Json("");
            }
            List<ValveState> valves = JsonConvert.DeserializeObject<List<ValveState>>(message);
            return Json(valves);
        }
    }
}
