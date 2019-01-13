﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public interface ICommentRepository
	{
		IQueryable<Comment> Comments { get; }

		void AddComment(Comment comment);
		void DeleteComment(int commentID);
	}
}
