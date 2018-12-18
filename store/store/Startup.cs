﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using store.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace store
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration["Data:storeProducts:ConnectionString"]));

			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddTransient<IOrderRepository, OrderRepository>();
			services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddMemoryCache();
			services.AddSession();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseSession();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: null,
					template: "{category}/Page{productPage:int}",
					defaults: new { Controller = "Product", action = "List" });

				routes.MapRoute(
					name: null,
					template: "Page{productPage:int}",
					defaults: new { Controller = "Product", action = "List", productPage = 1 });

				routes.MapRoute(
					name: null,
					template: "{category}",
					defaults: new { Controller = "Product", action = "List", productPage = 1 });

				routes.MapRoute(
					name: null,
					template: "",
					defaults: new { Controller = "Product", action = "List", productPage = 1 });

				routes.MapRoute(
					name: null,
					template: "{controller}/{action}/{id?}");
			});
			SeedData.EnsurePopulated(app);
		}
	}
}
