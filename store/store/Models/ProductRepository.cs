using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public class ProductRepository : IProductRepository
	{
		private ApplicationDbContext context;

		public ProductRepository(ApplicationDbContext c)
		{
			context = c;
		}

		public IQueryable<Product> Products => context.Products;

		public void EditProduct(Product product)
		{
			Product toedit = context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
			if (toedit != null)
			{
				toedit.Name = product.Name;
				toedit.Author = product.Author;
				toedit.Description = product.Description;
				toedit.Price = product.Price;
				toedit.Category = product.Category;
			}
			context.SaveChanges();
		}

		public void AddProduct(Product product)
		{
			context.Products.Add(product);
			context.SaveChanges();
		}

		public void DeleteProduct(int productID)
		{
			Product todel = context.Products.FirstOrDefault(p => p.ProductID == productID);
			if (todel != null)
			{
				context.Products.Remove(todel);
				context.SaveChanges();
			}
		}
	}
}
