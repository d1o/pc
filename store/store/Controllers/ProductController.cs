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
		private IProductRepository _productRepo;
		private ICommentRepository _commentRepo;
		public int PageSize = 100;

		public ProductController(IProductRepository rep, ICommentRepository com)
		{
			_productRepo = rep;
			_commentRepo = com;
		}

		public ViewResult List(string category, int productPage = 1, string search = null)
		{
			return View(new ProductsListViewModel
			{
				Products = _productRepo.Products
					.Where(p => category == null || p.Category == category)
					.Where(p => search == null || p.Name.Contains(search) || p.Author.Contains(search))
					.OrderBy(p => p.ProductID)
					.Skip((productPage - 1) * PageSize)
					.Take(PageSize),
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = _productRepo.Products.Count()
				},
				ChosenCategory = category
			});

		}

		public ViewResult Details(int productID, string returnUrl)
		{
			Product product = _productRepo.Products.FirstOrDefault(p => p.ProductID == productID);
			ViewBag.rUrl = returnUrl;
			ViewBag.imgPath = "~/images/prodcutsimg/" + product.ProductID.ToString() + ".jpg";
			List<Comment> comments = _commentRepo.Comments.Where(c => c.CommentedProdid == product.ProductID).ToList();

			return View(new DetailsViewModel
			{
				Product = product,
				Comments = comments
			});
		}

		public IActionResult AddComment(string author, string body, int productID, string returnUrl)
		{
			Product product = _productRepo.Products.FirstOrDefault(p => p.ProductID == productID);
			Comment c = new Comment
			{
				Author = author,
				Body = body,
				ProductName = product.Name,
				ProductAuthor = product.Author,
				CommentedProdid = productID
			};

			_commentRepo.AddComment(c);

			return RedirectToAction("Details", new { productID, returnUrl });
		}
	}
}