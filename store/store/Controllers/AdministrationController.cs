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
		private IProductRepository repositoryP;
		private ICommentRepository repositoryC;
		private UserManager<AppUser> userMgr;

		public AdministrationController(IProductRepository r, ICommentRepository c, UserManager<AppUser> u)
		{
			repositoryP = r;
			repositoryC = c;
			userMgr = u;
		}

		public ViewResult ManageProducts()
		{
			return View(repositoryP.Products);
		}

		public ViewResult EditProduct(int productID)
		{
			return View(repositoryP.Products.FirstOrDefault(p => p.ProductID == productID));
		}

		[HttpPost]
		public IActionResult EditProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				repositoryP.EditProduct(product);
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
				repositoryP.AddProduct(product);
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
			repositoryP.DeleteProduct(productId);
			return RedirectToAction("ManageProducts");
		}

		[HttpPost]
		public IActionResult DeleteCom(int commentID)
		{
			repositoryC.DeleteComment(commentID);
			return RedirectToAction("ManageComments");
		}

		public ViewResult ManageComments()
		{
			return View(repositoryC.Comments);
		}

		public ViewResult ManageUsers()
		{
			return View(userMgr.Users);
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

				IdentityResult result = await userMgr.CreateAsync(user, model.Password);
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
			AppUser u = await userMgr.FindByIdAsync(id);
			if (User != null)
			{
				IdentityResult result = await userMgr.DeleteAsync(u);
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
	}
}