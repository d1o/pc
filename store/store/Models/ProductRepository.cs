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
	}
}
