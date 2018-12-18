using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace store.Infrastructure
{
	public static class SessionExtensions
	{
		public static void SerCart(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T DesCart<T>(this ISession session, string key)
		{
			var sessionData = session.GetString(key);
			if (sessionData == null)
			{
				return default(T);
			}
			else
			{
				return JsonConvert.DeserializeObject<T>(sessionData);
			}
		}
	}
}
