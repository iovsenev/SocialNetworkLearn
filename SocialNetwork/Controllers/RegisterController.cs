using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Views.ViewModels;

namespace SocialNetwork.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View("Home/Register");
        }

        [HttpGet]
        [Route("RegisterPart2")]
        public IActionResult RegisterPart2(RegisterViewModel model)
        {
            return View("RegisterPart2", model);
        }
    }
}
