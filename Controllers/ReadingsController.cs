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
            if (filter == null)
            {
                return Json(new Error() { ErrorMessages = { "Filter is null!" } });
            }

            try
            {
                return Json(_readingRepository.GetAllByFilter(filter).Result.ToList());
            }
            catch (Exception)
            {
                return Json(new Error() { ErrorMessages = { "An error occured when fetching collectors' data!" } });
            }
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(new ReadingFilter() { CollectorId = id });
        }
    }
}
