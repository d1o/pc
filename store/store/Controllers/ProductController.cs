using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using store.Models;
using store.Models.ViewModels;

namespace store.Controllers
{	
    public class ProductController : Controller
    {
		private IProductRepository repository;
		public int PageSize = 100;

        public ProductController(IProductRepository rep)
		{
			repository = rep;
		}

		public ViewResult List(string category, int productPage = 1)
		{
			return View(new ProductsListViewModel
			{
				Products = repository.Products
					.Where(p => category == null || p.Category == category)
					.OrderBy(p => p.ProductID)
					.Skip((productPage - 1) * PageSize)
					.Take(PageSize),
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = repository.Products.Count()
				},
				ChosenCategpry = category
			});

		}

		public ViewResult Details(int productID, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
			ViewBag.rUrl = returnUrl;
			ViewBag.imgPath = "~/images/prodcutsimg/" + product.ProductID.ToString() + ".jpg";
			
			return View(product);
		}
    }
}