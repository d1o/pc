using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Adres email jest wymagany.")]
		[UIHint("email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Hasło jest wymagane.")]
		[UIHint("password")]
		public string Password { get; set; }
	}
}
