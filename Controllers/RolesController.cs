using IsIoTWeb.Models;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            List<Role> roles = new List<Role>();
            try
            {
                roles = (List<Role>)_roleRepository.GetAll().Result;
            }
            catch (Exception)
            {
                /* Do nothing. Return empty list of roles. */
            }
            return View(roles);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                try
                {
                    errors = await _roleRepository.Create(name);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

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
