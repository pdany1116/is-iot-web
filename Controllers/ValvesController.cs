using IsIoTWeb.Models;
using IsIoTWeb.Models.Valve;
using IsIoTWeb.Mqtt;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        private IUserRepository _userRepository;
        private IMqttClient _mqttClient;

        public ValvesController(IValveRepository valveRepository, IUserRepository userRepository, IMqttClient mqttClient)
        {
            _valveRepository = valveRepository;
            _userRepository = userRepository;
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
            List<ValveLog> valvesLogs = _valveRepository.GetAll().Result.ToList();

            if (filter == null)
            {
                return Json(valvesLogs);
            }
            else
            {
                if (filter.ValveId != null)
                {
                    valvesLogs = valvesLogs.Where(x => x.ValveId == filter.ValveId).ToList();
                }

                if (!string.IsNullOrEmpty(filter.OneDate))
                {
                    valvesLogs = valvesLogs.Where(x => x.Date() == filter.OneDate).ToList();
                }

                if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                {
                    DateTime fromDatetime = DateTime.Parse(filter.FromDate);
                    DateTime toDatetime = DateTime.Parse(filter.ToDate);
                    valvesLogs = valvesLogs.Where(x => DateTime.Parse(x.Date()) >= fromDatetime && DateTime.Parse(x.Date()) < toDatetime).ToList();
                }
            }

            List<ValveLogDisplay> valvesLogsDisplay = new List<ValveLogDisplay>();
            var users = await _userRepository.GetAll();

            foreach (var valveLog in valvesLogs)
            {
                ValveLogDisplay valveLogDisplay = new ValveLogDisplay();
                valveLogDisplay.ValveId = valveLog.ValveId;
                valveLogDisplay.Timestamp = valveLog.Timestamp;
                valveLogDisplay.Action = valveLog.Action;

                if (valveLog.UserId != null)
                {
                    var user = users.First(x => x.Id.ToString() == valveLog.UserId);
                    string fullName = user == null ? "" : user.FirstName + " " + user.LastName;
                    valveLogDisplay.User = new ValveLogDisplay.UserInfo(valveLog.UserId, fullName);
                }
                else
                {
                    valveLogDisplay.User = new ValveLogDisplay.UserInfo("", "");
                }

                valvesLogsDisplay.Add(valveLogDisplay);
            }

            return Json(valvesLogsDisplay);
        }

        public IActionResult Logs()
        {
            return View();
        }

        public async Task<IActionResult> Control()
        {
            await _mqttClient.Connect();
            RequestStatus();
            await _mqttClient.Subscribe("/valves/status/response/");
            return View();
        }

        public IActionResult Configure()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Action([FromBody] ValveActionInput valveActionInput)
        {
            await _mqttClient.Connect();

            var valveState = GetLastValvesState().Where(x => x.ValveId == valveActionInput.ValveId).FirstOrDefault();
            if (valveState == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Valve [{valveActionInput.ValveId}] does not exist.");
            }

            ValveAction valveAction = new ValveAction(valveActionInput.ValveId,
                    valveActionInput.Action,
                    _userRepository.GetLoggedUserByUsername(User.Identity.Name).Result.Id.ToString());

            await _mqttClient.Publish($"/valves/control/", JsonConvert.SerializeObject(valveAction, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            RequestStatus();

            if ((valveState.State == "ON" && valveActionInput.Action == "TURN_OFF") || (valveState.State == "OFF" && valveActionInput.Action == "TURN_ON"))
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            else if (valveState.State == "OFF" && valveActionInput.Action == "TURN_OFF")
            {
                return StatusCode((int)HttpStatusCode.Accepted, $"Request sent, but valve [{valveActionInput.ValveId}] was already turned off.");
            }
            else if (valveState.State == "ON" && valveActionInput.Action == "TURN_ON")
            {
                return StatusCode((int)HttpStatusCode.Accepted, $"Request sent, but valve [{valveActionInput.ValveId}] was already turned on.");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Accepted, $"Undefined.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> TurnAllOff()
        {
            await _mqttClient.Connect();
            List<ValveState> valvesStates = GetLastValvesState();
            foreach (var valve in valvesStates)
            {
                ValveAction action = new ValveAction(valve.ValveId,
                    "TURN_OFF",
                    _userRepository.GetLoggedUserByUsername(User.Identity.Name).Result.Id.ToString());

                await _mqttClient.Publish($"/valves/control/", JsonConvert.SerializeObject(action, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            }
            RequestStatus();
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<JsonResult> GetValvesState()
        {
            RequestStatus();
            return Json(GetLastValvesState());
        }

        private List<ValveState> GetLastValvesState()
        {
            var message = _mqttClient.GetLastPayload();
            List<ValveState> valves = new List<ValveState>();
            if (!string.IsNullOrEmpty(message))
            {
                valves = JsonConvert.DeserializeObject<List<ValveState>>(message);
            }
            return valves;
        }

        private async void RequestStatus()
        {
            await _mqttClient.Publish($"/valves/status/request/", "{}");
        }
    }
}
