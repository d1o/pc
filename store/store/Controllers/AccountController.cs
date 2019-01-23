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
		private UserManager<AppUser> userMgr;
		private SignInManager<AppUser> sIMgr;

		public AccountController(UserManager<AppUser> u, SignInManager<AppUser> s)
		{
			userMgr = u;
			sIMgr = s;
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
				AppUser u = await userMgr.FindByEmailAsync(model.Email);
				if (User != null)
				{
					await sIMgr.SignOutAsync();
					Microsoft.AspNetCore.Identity.SignInResult result = await sIMgr.PasswordSignInAsync(u, model.Password, false, false);
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
			await sIMgr.SignOutAsync();
			return Redirect("/");
		}
	}
}