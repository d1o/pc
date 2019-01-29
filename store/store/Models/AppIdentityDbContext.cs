using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace store.Models
{
	public class AppIdentityDbContext : IdentityDbContext<AppUser>
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
			: base(options)
		{ }

		public static async Task AddAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
		{
			UserManager<AppUser> userMgr = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			RoleManager<IdentityRole> roleMgr = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			string username = "admin@store.com";
			string email = "admin@store.com";
			string password = "admin1";
			string role = "Admin";

			if (await userMgr.FindByEmailAsync(email) == null)
			{
				if (await roleMgr.FindByNameAsync(role) == null)
				{
					await roleMgr.CreateAsync(new IdentityRole(role));
				}

				AppUser user = new AppUser
				{
					UserName = username,
					Email = email,
				};
				IdentityResult result = await userMgr.CreateAsync(user, password);

				if (result.Succeeded)
				{
					await userMgr.AddToRoleAsync(user, role);
				}
			}
		}
	}
}
