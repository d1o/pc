using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using store.Infrastructure;

namespace store.Models
{
	public class SessionCart : Cart
	{
		public static Cart GetCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

			SessionCart cart = session?.DesCart<SessionCart>("Cart") ?? new SessionCart();
			cart.Session = session;
			return cart;
		}

		[JsonIgnore]
		public ISession Session { get; set; }

		public override void AddItem(Product product, int quantity)
		{
			base.AddItem(product, quantity);
			Session.SerCart("Cart", this);
		}

		public override void AddOne(Product product)
		{
			base.AddOne(product);
			Session.SerCart("Cart", this);
		}

		public override void SubOne(Product product)
		{
			base.SubOne(product);
			Session.SerCart("Cart", this);
		}

		public override void RemoveItem(Product product)
		{
			base.RemoveItem(product);
			Session.SerCart("Cart", this);
		}

		public override void Clear()
		{
			base.Clear();
			Session.Remove("Cart");
		}
	}
}
