using IsIoTWeb.Models;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IsIoTWeb.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var result = _userRepository.GetAll().Result;
            return View(result);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserInputModel user)
        {
            if (ModelState.IsValid)
            {
                var errors = await _userRepository.Create(user);

                if (errors == null)
                {
                    ViewBag.Message = "User Created Successfully";
                }
                else
                {
                    foreach (string error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(user);
        }
    }
}
