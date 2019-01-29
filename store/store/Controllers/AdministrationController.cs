using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using store.Models;

namespace store.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdministrationController : Controller
    {
		private IProductRepository _productRepository;
		private ICommentRepository _commentRepository;
		private UserManager<AppUser> _userManager;
		private IPasswordValidator<AppUser> _passwordValidator;
		private IPasswordHasher<AppUser> _passwordHasher;

		public AdministrationController(IProductRepository productRepository, ICommentRepository commentRepository, UserManager<AppUser> userManager,
			IPasswordValidator<AppUser> passwordValidator, IPasswordHasher<AppUser> passwordHasher)
		{
			_productRepository = productRepository;
			_commentRepository = commentRepository;
			_userManager = userManager;
			_passwordValidator = passwordValidator;
			_passwordHasher = passwordHasher;
		}

		public ViewResult Index()
		{
			return View();
		}

		public ViewResult ManageProducts()
		{
			return View(_productRepository.Products);
		}

		public ViewResult EditProduct(int productID)
		{
			return View(_productRepository.Products.FirstOrDefault(p => p.ProductID == productID));
		}

		[HttpPost]
		public IActionResult EditProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				_productRepository.EditProduct(product);
				return RedirectToAction("ManageProducts");
			}
			else
			{
				return View(product);
			}
		}

		public ViewResult AddProduct()
		{
			return View(new Product());
		}

		[HttpPost]
		public IActionResult AddProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				_productRepository.AddProduct(product);
				return RedirectToAction("ManageProducts");
			}
			else
			{
				return View(product);
			}
		}

		[HttpPost]
		public IActionResult DeleteProd(int productId)
		{
			_productRepository.DeleteProduct(productId);
			return RedirectToAction("ManageProducts");
		}

		[HttpPost]
		public IActionResult DeleteCom(int commentID)
		{
			_commentRepository.DeleteComment(commentID);
			return RedirectToAction("ManageComments");
		}

		public ViewResult ManageComments()
		{
			return View(_commentRepository.Comments);
		}

		public ViewResult ManageUsers()
		{
			return View(_userManager.Users);
		}

		public ViewResult CreateUser()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CreateUser(CreateUserModel model)
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
					return RedirectToAction("ManageUsers");
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

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string id)
		{
			AppUser user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("ManageUsers");
				}
				else
				{
					foreach (IdentityError e in result.Errors)
					{
						ModelState.AddModelError("", e.Description);
					}
				}
			}
			else
			{
				ModelState.AddModelError("", "Nie znaleziono użytkownika");
			}
			
			//return View("ManageUsers", userMgr.Users);
			return View("ManageUsers");
		}

		public async Task<IActionResult> EditUser(string id)
		{
			AppUser user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				return View(user);
			}
			else
			{
				return RedirectToAction("ManageUsers");
			}
		}

		[HttpPost]
		//public async Task<IActionResult> EditUser(string id, string email, string password, string name, string street,
		//	string streetnumber, string housenumber, string city, string zip)
		public async Task<IActionResult> EditUser(string id, string email, string name, string password, string street,
			string streetnumber, string housenumber, string city, string zip)
		{
			AppUser user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				user.Email = email;
				user.UserName = email;
				user.Name = name;
				user.Street = street;
				user.StreetNumber = streetnumber;
				user.HouseNumber = housenumber;
				user.City = city;
				user.Zip = zip;
				
				if (!string.IsNullOrEmpty(password))
				{
					IdentityResult passwordResult = await _passwordValidator.ValidateAsync(_userManager, user, password);
					if (passwordResult.Succeeded)
					{
						user.PasswordHash = _passwordHasher.HashPassword(user, password);
					}
				}

				IdentityResult result = await _userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("ManageUsers");
				}
				else
				{
					foreach (IdentityError e in result.Errors)
					{
						ModelState.AddModelError("", e.Description);
					}
				}
			}
			return View(user);
		}
	}
}