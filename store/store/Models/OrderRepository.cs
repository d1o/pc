using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public class OrderRepository : IOrderRepository
	{
		private ApplicationDbContext context;

		public OrderRepository(ApplicationDbContext c)
		{
			context = c;
		}

		public IQueryable<Order> Orders => context.Orders.Include(o => o.Items).ThenInclude(p => p.Product);

		public void SaveOrder(Order order)
		{
			context.AttachRange(order.Items.Select(l => l.Product));
			if (order.OrderID == 0)
			{
				context.Orders.Add(order);
			}
			context.SaveChanges();
		}
	}
}
