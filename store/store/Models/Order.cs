using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public class Order
	{
		[BindNever]
		public int OrderID { get; set; }
		[BindNever]
		public ICollection<CartItem> Items { get; set; }


		[Required(ErrorMessage = "Należy podać imię i nazwisko odbiorcy")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Należy podać dane oznaczone *")]
		public string Street { get; set; }
		[Required(ErrorMessage = "Należy podać dane oznaczone *")]
		public string StreetNumber { get; set; }
		public string HouseNumber { get; set; }
		[Required(ErrorMessage = "Należy podać dane oznaczone *")]
		public string City { get; set; }
		[Required(ErrorMessage = "Należy podać dane oznaczone *")]
		public string Zip { get; set; }

		public int ShippingCost { get; set; } = 15;
	}
}
