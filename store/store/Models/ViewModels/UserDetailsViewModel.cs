using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models.ViewModels
{
	public class UserDetailsViewModel
	{
		public AppUser User { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
		public string ProductName { get; set; }
	}
}
