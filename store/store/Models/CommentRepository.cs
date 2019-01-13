using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public class CommentRepository : ICommentRepository
	{
		private ApplicationDbContext context;

		public CommentRepository(ApplicationDbContext c)
		{
			context = c;
		}

		public IQueryable<Comment> Comments => context.Comments;

		public void AddComment(Comment comment)
		{
			context.Add(comment);
			context.SaveChanges();
		}

		public void DeleteComment(int commentID)
		{
			Comment todel = context.Comments.FirstOrDefault(o => o.CommentID == commentID);
			if (todel != null)
			{
				context.Comments.Remove(todel);
				context.SaveChanges();
			}
			context.SaveChanges();
		}
	}
}
