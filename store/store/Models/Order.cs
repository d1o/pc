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

		public string Purchaser { get; set; }

		[Required(ErrorMessage = "Proszę podać imię i nazwisko odbiorcy")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Proszę podać ulicę")]
		public string Street { get; set; }
		[Required(ErrorMessage = "Proszę podać numer domu")]
		public string StreetNumber { get; set; }
		public string HouseNumber { get; set; }
		[Required(ErrorMessage = "Proszę podać miasto")]
		public string City { get; set; }
		[Required(ErrorMessage = "Proszę podać kod pocztowy")]
		public string Zip { get; set; }

		public int ShippingCost { get; set; } = 15;

		[BindNever]
		public bool IsShipped { get; set; }
	}
}
