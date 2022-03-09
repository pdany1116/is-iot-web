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
            List<Reading> list = _readingRepository.GetAll().Result.ToList();
            if (filter == null)
            {
                return Json(list);
            }
            else
            {
                if (filter.CollectorId != null)
                {
                    list = list.Where(x => x.CollectorId == filter.CollectorId).ToList();
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
    }
}
