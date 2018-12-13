using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using store.Models;

namespace store.Components
{
	public class NavMenuViewComponent : ViewComponent
	{
		private IProductRepository repository;

		public NavMenuViewComponent(IProductRepository rep)
		{
			repository = rep;
		}

		public IViewComponentResult Invoke()
		{
			ViewBag.SelectedCategory = RouteData?.Values["category"];
			return View(repository.Products.Select(r => r.Category).Distinct().OrderBy(r => r));

		}
	}
}
