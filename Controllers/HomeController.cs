using IsIoTWeb.Models;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IsIoTWeb.Controllers
{
    public class HomeController : Controller
    {
        private IReadingRepository _readingRepository;

        public HomeController(IReadingRepository readingRepository)
        {
            _readingRepository = readingRepository;
        }

        public IActionResult Index()
        {
            return View(_readingRepository.GetAll().Result);
        }

        public IActionResult Privacy()
        {
            // TODO: remove
            var obj = _readingRepository.Get("61e4afa83246c59bc696ec2e").Result;
            obj.AirHummidity = -1;
            _readingRepository.Update(obj);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
