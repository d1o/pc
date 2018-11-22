using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using store.Models;

namespace store.Controllers
{
    public class ProductController : Controller
    {
		private IProductRepository repository;

        public ProductController(IProductRepository rep)
		{
			repository = rep;
		}

		public ViewResult List() => View(repository.Products);
    }
}