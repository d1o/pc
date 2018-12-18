using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using store.Models;

namespace store.Components
{
	public class CartNavViewComponent : ViewComponent
	{
		private Cart cart;
		public CartNavViewComponent(Cart c)
		{
			cart = c;
		}

		public IViewComponentResult Invoke()
		{
			return View(cart);
		}
	}
}
