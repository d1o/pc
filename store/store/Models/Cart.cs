using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public class CartItem
	{
		public int CartItemID { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}

	public class Cart
	{
		private List<CartItem> Items { get; set; } = new List<CartItem>();

		public virtual void AddItem(Product product, int quantity)
		{
			CartItem item = Items.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();

			if (item == null)
			{
				Items.Add(new CartItem
				{
					Product = product,
					Quantity = quantity		
				});

			}
			else
			{
				item.Quantity += quantity;
			}
		}

		public virtual void AddOne(Product product)
		{
			CartItem item = Items.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
			item.Quantity += 1;
		}

		public virtual void SubOne(Product product)
		{
			CartItem item = Items.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
			item.Quantity -= 1;
		}

		public virtual void RemoveItem(Product product)
		{
			Items.RemoveAll(l => l.Product.ProductID == product.ProductID);
		}

		public virtual decimal TotalValue() => Items.Sum(e => e.Product.Price * e.Quantity);

		public virtual void Clear() => Items.Clear();

		public virtual IEnumerable<CartItem> AllItems => Items;
	}
}
