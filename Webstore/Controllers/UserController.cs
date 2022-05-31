using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Webstore.Domain.Entity;
using Webstore.Domain.Helper;
using Webstore.Domain.ViewModel.User;
using Webstore.Service.Interfaces;

namespace Webstore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Register(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Error", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    List<Item> items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                    for (var i = 0; i < items?.Count; i++)
                    {
                        items.RemoveAt(i);
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Error", response.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            List<Item> items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (var i = 0; i < items?.Count; i++)
            {
                items.RemoveAt(i);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            if (User.Identity.IsAuthenticated)
            {
                var response = await _userService.GetUserByEmail(User.Identity.Name);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return PartialView(response.Data);
                }
                ModelState.AddModelError("Error", response.Description);
            }
            ModelState.AddModelError("Error", "You are not authenticated");
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(user.Id_user, user);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
