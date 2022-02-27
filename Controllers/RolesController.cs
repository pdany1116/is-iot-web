using IsIoTWeb.Models;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IsIoTWeb.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private IRoleRepository _roleRepository;

        public RolesController( IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            var result = _roleRepository.GetAll().Result;
            return View(result);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                var errors = await _roleRepository.Create(name);
                if (errors == null)
                {
                    ViewBag.Message = "Role Created Successfully";
                }
                else
                {
                    foreach (string error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View();
        }
    }
}
