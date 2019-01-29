using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models.ViewModels
{
	public class DetailsViewModel
	{
		public Product Product { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
		public string CommentAuthor { get; set; }
	}
}
