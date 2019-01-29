using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using store.Models;
using store.Models.ViewModels;

namespace store.Controllers
{
	[Authorize]
	public class OrderController : Controller
    {
		private IOrderRepository _orderRepository;
		private Cart _cartService;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;



		public OrderController(IOrderRepository rep, Cart cartService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_orderRepository = rep;
			_cartService = cartService;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		/*public ViewResult Checkout()
        {
			return View(new Order());
        }

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			if (ModelState.IsValid)
			{
				order.Items = _cartService.AllItems.ToArray();
				_orderRepository.SaveOrder(order);
				return RedirectToAction("Done", new { order="order" });
			}
			else
			{
				return View(order);
			}
		}*/

		/*
		public async Task<IActionResult> Checkout()
		{
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);
			ViewBag.Purchaser = user.Email;
			return View(new Order());
		}
		*/

		public async Task<IActionResult> Checkout()
		{
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);
			ViewBag.Purchaser = user.Id;
			Order order = new Order()
			{
				Purchaser = user.Email,
				Name = user.Name,
				Street = user.Street,
				StreetNumber = user.StreetNumber,
				HouseNumber = user.HouseNumber,
				City = user.City,
				Zip = user.Zip
			};
			return View(order);
		}

		[HttpPost]
		public async Task<IActionResult> Checkout(Order order)
		{
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);
			ViewBag.Purchaser = user.Id;
			if (ModelState.IsValid)
			{
				order.Items = _cartService.AllItems.ToArray();
				_orderRepository.SaveOrder(order);
				return RedirectToAction("Done", new { order = "order" });
			}
			else
			{
				return View(order);
			}
		}

		public ViewResult Done(Order order)
		{
			ViewData["TotalCost"] = _cartService.TotalValue() + order.ShippingCost;
		
			_cartService.Clear();
			return View(order);
		}

		[Authorize(Roles = "Admin")]
		public ViewResult ListOfOrders()
		{
			var LOOVM = new List<ListOfOrdersViewModel>();
			foreach (var o in _orderRepository.Orders)
			{
				decimal t = 0;
				foreach (var item in o.Items)
				{
					t += item.Quantity * item.Product.Price;
				};

				LOOVM.Add(new ListOfOrdersViewModel
				{
					order = o,
					total = t + 15
				});
			}
			return View(LOOVM);
		}

		[HttpPost]
		public IActionResult ChangeToShipped(int orderID)
		{
			Order order = _orderRepository.Orders.FirstOrDefault(o => o.OrderID == orderID);
			if (order != null)
			{
				order.IsShipped = true;
				_orderRepository.SaveOrder(order);
			}
			return RedirectToAction(nameof(ListOfOrders));
		}
	}
}