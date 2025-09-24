using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
	public class AuthDbContext:IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "2b60649a-ea82-48ae-a457-5095aca59dfa";
			var writerRoleId = "04084858-ae38-4531-9c8f-d61901e8ab35";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id = readerRoleId,
					Name = "Reader",
					NormalizedName = "reader",
					ConcurrencyStamp = readerRoleId
				},
				new IdentityRole
				{
					Id = writerRoleId,
					Name = "Writer",
					NormalizedName = "writer",
					ConcurrencyStamp = writerRoleId
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);

			//create an admin role
			var adminUserId = "e601ade8-a5b0-4846-8b5c-fb92df36f480";

			var admin = new IdentityUser
			{
				Id = adminUserId,
				UserName = "admin",
				NormalizedUserName = "ADMIN".ToUpper(),
				Email = "admin@admin.com",
				NormalizedEmail = "admin@admin.com".ToUpper(),
			};

			admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

			builder.Entity<IdentityUser>().HasData(admin);

			var adminRoles = new List<IdentityUserRole<string>>
			{
				new ()
				{
					UserId = adminUserId,
					RoleId = readerRoleId
				},
				new ()
				{
					UserId = adminUserId,
					RoleId = writerRoleId
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
		}
	}
}

