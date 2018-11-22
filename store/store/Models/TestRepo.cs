using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public class TestRepo : IProductRepository
	{
		public IQueryable<Product> Products => new List<Product>
		{
			new Product { Name = "Dziady. Część II", Author = "Adam Mickiewicz",  Description = "Wydanie kompletne, bez skrótów i cięć w treści.", Price = 5.49m, Category = "Lektury" },
			new Product { Name = "Konrad Wallenrod", Author = "Adam Mickiewicz",  Description = "Wydanie kompletne, bez skrótów i cięć w treści. ", Price = 4.99m, Category = "Lektury" },
			new Product { Name = "Solaris", Author = "Stanisław Lem",  Description = "W książce Stanisław Lem podejmuje jeden z najpopularniejszych tematów literatury fantastycznej - czyli kontaktu, z obcą cywilizacją, odmienną formą życia, i nieznanym.", Price = 30.99m, Category = "Sciene Fiction" }
		}.AsQueryable<Product>();
	}
}
