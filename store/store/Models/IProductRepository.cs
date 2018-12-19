using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public interface IProductRepository
	{
		IQueryable<Product> Products { get; }

		void EditProduct(Product product);
		void AddProduct(Product product);
		void DeleteProduct(int productID);
	}
}
