using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using store.Models;
using store.Models.ViewModels;

namespace store.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private UserManager<AppUser> _userManager;
		private SignInManager<AppUser> _signInManager;
		private ICommentRepository _commentRepository;


		public AccountController(UserManager<AppUser> userMnager, SignInManager<AppUser> signInManager, ICommentRepository commentRepository)
		{
			_userManager = userMnager;
			_signInManager = signInManager;
			_commentRepository = commentRepository;
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
				AppUser user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					await _signInManager.SignOutAsync();
					Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
					if (result.Succeeded)
					{
						return Redirect(returnUrl ?? "/");
					}
				}
				ModelState.AddModelError(nameof(LoginModel.Email), "Niepoprawny email lub hasło");
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

		public RedirectToActionResult MoveToProduct(int commentedProductId)
		{
			return RedirectToAction("Details", "Product", new { productID = commentedProductId, returnUrl = "/" });
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

		public async Task<IActionResult> Details()
		{
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);
			return View(new UserDetailsViewModel
			{
				User = user,
				Comments = _commentRepository.Comments.Where(c => c.Author == user.Email).ToList()
		});
		}

		public async Task<IActionResult> EditDetails()
		{
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);
			return View(user);
		}

		[HttpPost]
		public async Task<IActionResult> EditDetails(string name, string street, string streetnumber, string housenumber, string city, string zip)
		{
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);

			user.Name = name;
			user.Street = street;
			user.StreetNumber = streetnumber;
			user.HouseNumber = housenumber;
			user.City = city;
			user.Zip = zip;
			await _userManager.UpdateAsync(user);

			return RedirectToAction("Details");
		}
	}
}