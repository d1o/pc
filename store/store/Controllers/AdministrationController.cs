using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using store.Models;

namespace store.Controllers
{
    public class AdministrationController : Controller
    {
		private IProductRepository repositoryP;
		private ICommentRepository repositoryC;

		public AdministrationController(IProductRepository r, ICommentRepository c)
		{
			repositoryP = r;
			repositoryC = c;
		}

		public ViewResult ManageProducts()
		{
			return View(repositoryP.Products);
		}

		public ViewResult Edit(int productID)
		{
			return View(repositoryP.Products.FirstOrDefault(p => p.ProductID == productID));
		}

		[HttpPost]
		public IActionResult EditProd(Product product)
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

		public ViewResult Add()
		{
			return View(new Product());
		}

		[HttpPost]
		public IActionResult Add(Product product)
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
	}
}