using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using store.Models;
using store.Models.ViewModels;
using store.Infrastructure;

namespace store.Controllers
{
    public class CartController : Controller
    {
		private IProductRepository repository;
		private Cart cart;

		public CartController(IProductRepository rep, Cart cartService)
		{
			repository = rep;
			cart = cartService;
		}

		public ViewResult Index(string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}

		public ViewResult Confirmation(string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}

		/*public RedirectToActionResult AddToCart(int productId, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

			if (product != null)
			{
				cart.AddItem(product, 1);
			}
			//return RedirectToAction("Index", new { returnUrl });
		}*/

		public RedirectResult AddToCart(int productId, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

			if (product != null)
			{
				cart.AddItem(product, 1);
			}
			return Redirect(returnUrl);
		}

		public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

			if (product != null)
			{
				cart.RemoveItem(product);
			}
			return RedirectToAction("Index", new { returnUrl });
		}
		
		public RedirectToActionResult MoreProducts(int productId, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
			if (product != null)
			{
				cart.AddOne(product);
			}
			return RedirectToAction("Index", new { returnUrl });
		}

		public RedirectToActionResult LessProducts(int productId, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
			if (product != null)
			{
				cart.SubOne(product);
			}
			return RedirectToAction("Index", new { returnUrl });
		}
	}
}