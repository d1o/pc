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
		private IProductRepository repositoryP;
		private ICommentRepository repositoryC;
		public int PageSize = 100;

        public ProductController(IProductRepository rep, ICommentRepository com)
		{
			repositoryP = rep;
			repositoryC = com;
		}

		public ViewResult List(string category, int productPage = 1)
		{
			return View(new ProductsListViewModel
			{
				Products = repositoryP.Products
					.Where(p => category == null || p.Category == category)
					.OrderBy(p => p.ProductID)
					.Skip((productPage - 1) * PageSize)
					.Take(PageSize),
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = repositoryP.Products.Count()
				},
				ChosenCategory = category
			});

		}

		public ViewResult Details(int productID, string returnUrl)
		{
			Product product = repositoryP.Products.FirstOrDefault(p => p.ProductID == productID);
			ViewBag.rUrl = returnUrl;
			ViewBag.imgPath = "~/images/prodcutsimg/" + product.ProductID.ToString() + ".jpg";
			List<Comment> C = repositoryC.Comments.Where(c => c.CommentedProdid == product.ProductID).ToList();

			return View(new DetailsViewModel
			{
				Product = product,
				Comments = C
			});
		}

		public IActionResult AddComment(string author, string body, int productID, string returnUrl)
		{
			Comment c = new Comment
			{
				Author = author,
				Body = body,
				CommentedProdid = productID
			};

			repositoryC.AddComment(c);

			return RedirectToAction("Details", new { productID, returnUrl });
		}
    }
}