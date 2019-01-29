using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models.ViewModels
{
	public class ListOfOrdersViewModel
	{
		public Order order { get; set; }
		public decimal total { get; set; }
	}
}
