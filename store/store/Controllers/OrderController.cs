using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using store.Models;
using store.Models.ViewModels;

namespace store.Controllers
{
	[Authorize]
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

		[Authorize(Roles = "Admin")]
		public ViewResult ListOfOrders()
		{
			var LOOVM = new List<ListOfOrdersViewModel>();
			foreach (var o in repository.Orders)
			{
				decimal t = 0;
				foreach (var item in o.Items)
				{
					t += item.Quantity * item.Product.Price;
				};

				LOOVM.Add(new ListOfOrdersViewModel
				{
					order = o,
					total = t
				});
			}
			return View(LOOVM);
		}

		[HttpPost]
		public IActionResult ChangeToShipped(int orderID)
		{
			Order order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
			if (order != null)
			{
				order.IsShipped = true;
				repository.SaveOrder(order);
			}
			return RedirectToAction(nameof(ListOfOrders));
		}
	}
}