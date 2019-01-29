using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace store.Models
{
	public class AppUser : IdentityUser
	{
		public string Name { get; set; }
		public string Street { get; set; }
		public string StreetNumber { get; set; }
		public string HouseNumber { get; set; }
		public string City { get; set; }
		public string Zip { get; set; }
	}
}
