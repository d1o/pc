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
		private IProductRepository repository;

		public AdministrationController(IProductRepository r)
		{
			repository = r;
		}

		public ViewResult ManageProducts()
		{
			return View(repository.Products);
		}

		public ViewResult Edit(int productID)
		{
			return View(repository.Products.FirstOrDefault(p => p.ProductID == productID));
		}

		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				repository.EditProduct(product);
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
				repository.AddProduct(product);
				return RedirectToAction("ManageProducts");
			}
			else
			{
				return View(product);
			}
		}

		[HttpPost]
		public IActionResult Delete(int productId)
		{
			repository.DeleteProduct(productId);
			return RedirectToAction("ManageProducts");
		}
	}
}