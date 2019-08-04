using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SpotOn.ApplicationLogic.Entities.Users;
using SpotOn.ApplicationLogic.Interfaces;
using SpotOn.ApplicationLogic.ViewModels.Users;

namespace SpotOn.Controllers
{
    [Route("/auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        public IActionResult LogIn()
        {
            var loggedInUser = HttpContext.User.Claims;

            if (loggedInUser.Count() < 0)
                return RedirectToAction("Index", "Post");

            return View(new LogInViewModel());
        }

        [Route("login")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.LogInAsync(_mapper.Map<LogInEntity>(model));
            if (user == null)
            {
                ModelState.AddModelError("InvalidCredentials", "Could not validate your credentials");
                return View(model);
            }

            return await SignInUser(user);
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("register")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userService.RegisterAsync(_mapper.Map<RegisterUserEntity>(model));

            return await SignInUser(user);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        private async Task<IActionResult> SignInUser(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Post");
        }
    }
}