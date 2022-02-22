using IsIoTWeb.Models;
using IsIoTWeb.Mqtt;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IsIoTWeb.Controllers
{
    public class HomeController : Controller
    {
        private IReadingRepository _readingRepository;
        private IMqttClient _mqttClient;

        public HomeController(IReadingRepository readingRepository, IMqttClient mqttClient)
        {
            _readingRepository = readingRepository;
            _mqttClient = mqttClient;
        }

        public IActionResult Index()
        {
            return View(_readingRepository.Get("62103f33e05e2b1e724c4bf4").Result);
        }

        public async Task<IActionResult> Privacy()
        {
            await _mqttClient.Connect();
            await _mqttClient.Publish("/test/dani123/", "hello_vs");
            await _mqttClient.Disconnect();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
