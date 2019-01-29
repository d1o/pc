using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public class Comment
	{
		public int CommentID { get; set; }

		public string Author { get; set; }
		public string ProductName { get; set; }
		public string ProductAuthor { get; set; }
		public string Body { get; set; }
		public int CommentedProdid { get; set; }
	}
}
