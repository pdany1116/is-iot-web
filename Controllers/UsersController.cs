using IsIoTWeb.Models;
using IsIoTWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        public IActionResult Index()
        {
            var result = _userRepository.GetAll().Result;
            return View(result);
        }

        public ActionResult GetUsers()
        {
            var list = _userRepository.GetAll().Result.ToList();
            return Json(list);
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

        public ViewResult Edit() => View();

        [HttpPost]
        public IActionResult GetId([FromBody] UserDynamicId id)
        {
            return Json(new { id = new ObjectId(id.Timestamp, id.Machine, id.Pid, id.Increment).ToString() });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.Get(id);
            UserInputModel userInputModel = new UserInputModel();
            userInputModel.FirstName = user.FirstName;
            userInputModel.LastName = user.LastName;
            userInputModel.Email = user.Email;
            userInputModel.PhoneNumber = user.PhoneNumber;
            userInputModel.Username = user.UserName;
            return View(userInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserInputModel userInputModel)
        {
            if (ModelState.IsValid)
            {
                var errors = await _userRepository.Update(userInputModel);

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
