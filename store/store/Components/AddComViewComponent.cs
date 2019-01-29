using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using store.Models;
using store.Models.ViewModels;

namespace store.Components
{
	public class AddComViewComponent : ViewComponent
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AddComViewComponent(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IViewComponentResult> InvokeAsync(int productId, string returnUrl)
		{
			if (_signInManager.IsSignedIn(HttpContext.User))
			{
				AppUser user = await _userManager.GetUserAsync(HttpContext.User);
				List<string> ComInfo = new List<string> { productId.ToString(), returnUrl, user.Id };
				return View("LoggedIn", ComInfo);
			}
			else
			{
				return View();
			}
		}
	}
}
