using IsIoTWeb.Models;
using IsIoTWeb.Models.Valve;
using IsIoTWeb.Mqtt;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

        [HttpPost]
        public async Task<ActionResult> GetValveLogsByFilter([FromBody] ValveLogsFilter? filter)
        {
            await _mqttClient.Connect();
            List<ValveLog> list = _valveRepository.GetAll().Result.ToList();
            
            if (filter == null)
            {
                return Json(list);
            }
            else
            {
                if (filter.ValveId != null)
                {
                    list = list.Where(x => x.ValveId == filter.ValveId).ToList();
                }

                if (!string.IsNullOrEmpty(filter.OneDate))
                {
                    list = list.Where(x => x.Date() == filter.OneDate).ToList();
                }

                if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                {
                    DateTime fromDatetime = DateTime.Parse(filter.FromDate);
                    DateTime toDatetime = DateTime.Parse(filter.ToDate);
                    list = list.Where(x => DateTime.Parse(x.Date()) >= fromDatetime && DateTime.Parse(x.Date()) < toDatetime).ToList();
                }
            }
            return Json(list);
        }

        public IActionResult Logs()
        {
            return View();
        }

        public async Task<IActionResult> Control()
        {
            await _mqttClient.Connect();
            await _mqttClient.Publish($"/valves/status/request", "");
            await _mqttClient.Subscribe("/valves/status/response");
            var message = _mqttClient.GetLastPayload();
            return View();
        }

        public IActionResult Configure()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Action([FromBody] ValveAction valve)
        {
            await _mqttClient.Connect();
            var valveState = GetLastValvesState().Where(x => x.ValveId == valve.ValveId).FirstOrDefault();
            if (valveState == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Valve [{valve.ValveId}] does not exist.");
            }

            if ((valveState.State == "ON" && valve.Action == "TURN_OFF") || (valveState.State == "OFF" && valve.Action == "TURN_ON"))
            {
                await _mqttClient.Publish($"/valves/{valve.ValveId}", valve.Action);
                return StatusCode((int)HttpStatusCode.OK);
            }
            else if (valveState.State == "OFF" && valve.Action == "TURN_OFF")
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Valve [{valve.ValveId}] is already turned off.");
            }
            else if (valveState.State == "ON" && valve.Action == "TURN_ON")
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Valve [{valve.ValveId}] is already turned on.");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Undefined.");
            }
        }

        [HttpPost]
        public JsonResult GetValvesState()
        {
            return Json(GetLastValvesState());
        }

        public List<ValveState> GetLastValvesState()
        {
            var message = _mqttClient.GetLastPayload();
            List<ValveState> valves = new List<ValveState>();
            if (!string.IsNullOrEmpty(message))
            {
                valves = JsonConvert.DeserializeObject<List<ValveState>>(message);
            }
            return valves;
        }
    }
}
