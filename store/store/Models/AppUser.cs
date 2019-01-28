using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace store.Models
{
	public class AppUser : IdentityUser
	{
		public string Name { get; set; } = "test imie";
		public string Street { get; set; } = "test ulica";
		public string StreetNumber { get; set; } = "test numer domu1";
		public string HouseNumber { get; set; } = "test numer domu2";
		public string City { get; set; } = "test miasto";
		public string Zip { get; set; } = "test kod pocztowy";
	}
}
