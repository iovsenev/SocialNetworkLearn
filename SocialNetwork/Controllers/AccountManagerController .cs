using AutoMapper;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SocialNetwork.Models;
using static SocialNetwork.Mappers.UserFromModel;
using static SocialNetwork.Helpers.ConsoleWriter;

namespace SocialNetwork.Controllers
{
    public class AccountManagerController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountManagerController(IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Home/Login");
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                var result = await _signInManager.PasswordSignInAsync(
                    user.Email,
                    model.Password,
                    model.RememberMe,
                    false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("UserMainPage", "AccountManager");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            Console.WriteLine("Logout method");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("[controller]/[action]")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserMainPage()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var model = new UserViewModel(result)
            {
                Friends = await GetAllFriends(result)
            };
            return View("User", model);
        }

        private async Task<List<User>> GetAllFriends(User user)
        {
            var repository = _unitOfWork.GetRepository<Friend>() as FriendRepository;
            var users = await repository
                .GetFriendsByUserAsync(user);
            return users.Take(3).ToList();
        }

        private async Task<List<User>> GetAllFriends()
        {
            var user = await _userManager.GetUserAsync(User);
            var repository = _unitOfWork.GetRepository<Friend>() as FriendRepository;
            return await repository.GetFriendsByUserAsync(user);
        }

        [Authorize]
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Is valid");
                var user = await _userManager.FindByIdAsync(model.Id);
                Console.WriteLine("User is finded");
                user.Convert(model);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    Console.WriteLine("update succeeded");
                    return RedirectToAction("UserMainPage", "AccountManager");
                }
                else
                {
                    Console.WriteLine("updater is NOT succeeded");
                    return RedirectToAction("UpdateUser", "AccountManager");
                }
            }
            else
            {
                Console.WriteLine("model is NOT valid");
                ModelState.AddModelError("", "Некорректные данные");
                return View("UpdateUser", model);
            }
        }

        [Authorize]
        [Route("[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var userView = _mapper.Map<User, UpdateUserViewModel>(user);
            return View("UpdateUser", userView);
        }

        [Authorize]
        [Route("UserList")]
        [HttpPost]
        public async Task<IActionResult> UserList(string search)
        {
            var model = await CreateSearch(search);

            return View("UserList", model);
        }

        private async Task<SearchViewModel> CreateSearch(string search)
        {
            var currentUser = User;

            var result = await _userManager.GetUserAsync(currentUser);

            var list = _userManager.Users
                .AsEnumerable()
                .Where(x => x.GetFullName()
                .Contains(search, StringComparison.CurrentCultureIgnoreCase))
                .ToList();

            var withFriend = await GetAllFriend();

            var data = new List<UserWithFriendExt>();

            list.ForEach(x =>
            {
                if (x.Id != result.Id)
                {
                    var t = _mapper.Map<UserWithFriendExt>(x);
                    t.IsFriendWithCurrent = withFriend
                    .Where(y => y.Id == x.Id)
                    .Count() != 0;
                    data.Add(t);
                }
            });

            var model = new SearchViewModel()
            {
                UserList = data.Where(x => !x.IsFriendWithCurrent).ToList(),
            };

            return model;
        }

        private async Task<List<User>> GetAllFriend()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendRepository;

            return await repository.GetFriendsByUserAsync(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFriend(string id)
        {
            var user = await _userManager.GetUserAsync(User);

            var friend = await _userManager.FindByIdAsync(id);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendRepository;

            await repository.AddFriendAsync(user, friend);

            return RedirectToAction("UserMainPage", "AccountManager");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByIdAsync(id);
            var repository = _unitOfWork.GetRepository<Friend>() as FriendRepository;
            await repository.DeleteFriendAsync(user, friend);

            return RedirectToAction("UserMainPage", "AccountManager");
        }
    }
}
