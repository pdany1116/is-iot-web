using IsIoTWeb.Models;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IsIoTWeb.Controllers
{
    [Authorize]
    public class ReadingsController : Controller
    {
        private IReadingRepository _readingRepository;

        public ReadingsController(IReadingRepository readingRepository)
        {
            _readingRepository = readingRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetReadingsByFilter([FromBody] ReadingFilter? filter)
        {
            return Json(_readingRepository.GetAllByFilter(filter).Result.ToList());
        }
    }
}
