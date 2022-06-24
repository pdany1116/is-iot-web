using IsIoTWeb.Models;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Index() => View();

        public ViewResult Create() => View();

        public ViewResult Details() => View();

        public ViewResult Edit() => View();

        public ActionResult GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                users = (List<User>)_userRepository.GetAll().Result;
            }
            catch (Exception)
            {
                return Json(new Error() { ErrorMessages = { "An error occured when fetching Users' data!" } });
            }
            return Json(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInputModel user)
        {
            if (ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                try
                {
                    errors = await _userRepository.Create(user);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

                if (errors != null)
                {
                    foreach (string error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(user);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetId([FromBody] UserDynamicId id)
        {
            return Json(new { id = Utils.Utils.DynamicObjectIdToString(id.Timestamp, id.Machine, id.Pid, id.Increment) });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user;
            UserInputModel userInputModel = new UserInputModel();
            try
            {
                user = await _userRepository.Get(id);
                userInputModel.FirstName = user.FirstName;
                userInputModel.LastName = user.LastName;
                userInputModel.Email = user.Email;
                userInputModel.PhoneNumber = user.PhoneNumber;
                userInputModel.Username = user.UserName;
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            return View(userInputModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            User user;
            try
            {
                user = await _userRepository.Get(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userRepository.Delete(id);
            }
            catch (Exception)
            {
                /* Do nothing. Return to index. */
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserInputModel userInputModel)
        {
            if (ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                try
                {
                    errors = await _userRepository.Update(userInputModel);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

                if (errors == null)
                {
                    ViewBag.Message = "User Updated Successfully";
                }
                else
                {
                    foreach (string error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(userInputModel);
        }
    }
}
