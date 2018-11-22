using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace store.Models
{
	public class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
			context.Database.Migrate();
			if (!context.Products.Any())
			{
				context.Products.AddRange(
					new Product
					{
						Name = "Dziady. Część II",
						Author = "Adam Mickiewicz",
						Description = "Wydanie kompletne, bez skrótów i cięć w treści.",
						Price = 5.49m,
						Category = "Lektury"
					},
					new Product
					{
						Name = "Konrad Wallenrod",
						Author = "Adam Mickiewicz",
						Description = "Wydanie kompletne, bez skrótów i cięć w treści.",
						Price = 4.99m,
						Category = "Lektury"
					},
					new Product
					{
						Name = "Balladyna",
						Author = "Juliusz Słowacki",
						Description = "Wydanie kompletne, bez skrótów i cięć w treści.",
						Price = 9.89m,
						Category = "Lektury"
					},
					new Product
					{
						Name = "Instalacje elektryczne",
						Author = "Henryk Markiewicz",
						Description = "Dziewiąte wydanie cenionej i popularnej książki dotyczącej instalacji elektrycznych.",
						Price = 79.99m,
						Category = "Nauka"
					},
					new Product
					{
						Name = "Mały Książę",
						Author = "Antoine de Saint-Exupery",
						Description = "Od 1943 roku jest wydawany w milionach egzemplarzy na całym świecie.",
						Price = 11.99m,
						Category = "Lektury"
					},
					new Product
					{
						Name = "Solaris",
						Author = "Stanisław Lem",
						Description = "W książce Stanisław Lem podejmuje jeden z najpopularniejszych tematów literatury fantastycznej - czyli kontaktu, z obcą cywilizacją, odmienną formą życia, i nieznanym",
						Price = 30.99m,
						Category = "Sciene Fiction"
					},
					new Product
					{
						Name = "Metro 2033 ",
						Author = "Dmitry Glukhovsky",
						Description = "Nowe wydanie pierwszego tomu fantastyczno-naukowego cyklu \"Metro\" Dmitrija Glukhovsky’ego – \"Metro 2033\".",
						Price = 36.49m,
						Category = "Sciene Fiction"
					},
					new Product
					{
						Name = "Bastion",
						Author = "Stephen King",
						Description = "Przerażająca wizja świata po zagładzie biologicznej.",
						Price = 41.99m,
						Category = "Sciene Fiction"
					},
					new Product
					{
						Name = "Wielka gramatyka języka angielskiego z ćwiczeniami.",
						Author = "Opracowanie zbiorcze",
						Description = "Przystępne omówienie wszystkich ważnych zagadnień, aż 400 ćwiczeń utrwalających oraz wyraziste przykłady użycia teorii w praktyce.",
						Price = 33.49m,
						Category = "Nauka"
					},
					new Product
					{
						Name = "Atlas anatomiczny",
						Author = "Peter Abrahams",
						Description = "Jasne i zwięzłe opisy pozwalające na łatwe zrozumienie omawianych zagadnień.",
						Price = 30.99m,
						Category = "Nauka"
					}
				);
				context.SaveChanges();
			}
		}
	}
}
