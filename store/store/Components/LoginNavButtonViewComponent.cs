using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Components
{
	public class LoginNavButtonViewComponent : ViewComponent
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public LoginNavButtonViewComponent(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			if (_signInManager.IsSignedIn(HttpContext.User))
			{
				AppUser user = await _userManager.GetUserAsync(HttpContext.User);
				return View("LoggedIn", user);
			}
			else
			{
				return View();
			}
		}

		/*public IViewComponentResult Invoke()
		{
			return View();
		}*/
	}
}
