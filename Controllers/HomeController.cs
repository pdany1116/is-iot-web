using IsIoTWeb.Models;
using IsIoTWeb.Mqtt;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IsIoTWeb.Controllers
{
    [Authorize]
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
            return View();
        }
    }
}
