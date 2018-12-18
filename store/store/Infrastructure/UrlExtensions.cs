using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Infrastructure
{
	public static class UrlExtensions
	{
		public static string PathAndQuery(this HttpRequest request)
		{
			if (request.QueryString.HasValue)
			{
				return $"{request.Path}{request.QueryString}";
			}
			else
			{
				return $"{request.Path}";
			}
		}
	}
}
