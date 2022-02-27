using IsIoTWeb.Models;
using IsIoTWeb.Mqtt;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IsIoTWeb.Controllers
{
    [Authorize]
    public class ValvesController : Controller
    {
        private const int ValvesCount = 6;
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

        public async Task<IActionResult> LogsAsync()
        {
            var result = await _valveRepository.GetAll();
            return View(result);
        }

        public IActionResult Control()
        {
            // TODO: Request from sink valves states
            List<ValveState> valves = new List<ValveState>();

            for (int i = 0; i < ValvesCount; i++)
            {
                var valve = new ValveState();
                valve.ValveId = i;
                valve.State = "ON";
                valves.Add(valve);
            }
            return View(valves);
        }

        public IActionResult Configure()
        {
            return View();
        }

        public async Task<RedirectToActionResult> Publish(ValveState valve)
        {
            await _mqttClient.Connect();
            await _mqttClient.Publish("/test/dani123/", $"hello_vs {valve.ValveId}");
            await _mqttClient.Disconnect();
            return RedirectToAction("Control");
        }
    }
}
