using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using store.Models;

namespace store.Controllers
{
    public class OrderController : Controller
    {
		private IOrderRepository repository;
		private Cart cart;

		public OrderController(IOrderRepository rep, Cart cartService)
		{
			repository = rep;
			cart = cartService;
		}

		public ViewResult Checkout()
        {
            return View(new Order());
        }

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			if (ModelState.IsValid)
			{
				order.Items = cart.AllItems.ToArray();
				repository.SaveOrder(order);
				return RedirectToAction("Done", new { order="order" });
			}
			else
			{
				return View(order);
			}
		}

		public ViewResult Done(Order order)
		{
			ViewData["TotalCost"] = cart.TotalValue() + order.ShippingCost;
		
			cart.Clear();
			return View(order);
		}
    }
}