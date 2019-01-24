using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using store.Models;

namespace store.Controllers
{
	[Authorize]
	public class AccountController : Controller
    {
		private UserManager<AppUser> _userManager;
		private SignInManager<AppUser> _signInManager ;

		public AccountController(UserManager<AppUser> u, SignInManager<AppUser> s)
		{
			_userManager = u;
			_signInManager = s;
		}

		[AllowAnonymous]
		public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
			return View();
        }

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				AppUser u = await _userManager.FindByEmailAsync(model.Email);
				if (User != null)
				{
					await _signInManager.SignOutAsync();
					Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(u, model.Password, false, false);
					if (result.Succeeded)
					{
						return Redirect(returnUrl ?? "/");
					}
				}
				ModelState.AddModelError(nameof(LoginModel.Email), "niepoprawny email lub hasło");
			}
			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Redirect("/");
		}

		[AllowAnonymous]
		public ViewResult Register()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Register(CreateUserModel model)
		{
			if (ModelState.IsValid)
			{
				AppUser user = new AppUser
				{
					Email = model.Email,
					UserName = model.Email
				};

				IdentityResult result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("List", "Product");
				}
				else
				{
					foreach (IdentityError e in result.Errors)
					{
						ModelState.AddModelError("", e.Description);
					}
				}
			}
			return View(model);
		}

		public async Task<IActionResult> Details(string userId, string returnUrl)
		{
			if (_signInManager.IsSignedIn(HttpContext.User))
			{
				AppUser user = await _userManager.GetUserAsync(HttpContext.User);
				return View(user);
			}
			else
			{
				return RedirectToAction(returnUrl);

			}
		}	
	}
}