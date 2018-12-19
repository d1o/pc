using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace store.Models
{
	public class Product
	{
		public int ProductID { get; set; }

		[Required(ErrorMessage = "Nazwa jest wymagana")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Autor jest wymagany")]
		public string Author { get; set; }

		[Required(ErrorMessage = "Opis jest wymagany")]
		public string Description { get; set; }
		
		[Required]
		[Range(0.01, double.PositiveInfinity, ErrorMessage = "Niepoprawna cena")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Kategoria jest wymagana")]
		public string Category { get; set; }
	}
}
