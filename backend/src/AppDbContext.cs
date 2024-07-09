namespace SkyExplorer;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions options, JwtOptions jwtOptions) : DbContext(options) {
	public DbSet<AppUser> Users { get; set; }
	public DbSet<Plane> Planes { get; set; }
	public DbSet<Message> Messages { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Activity> Activities { get; set; }
	public DbSet<Flight> Flights { get; set; }
	public DbSet<Bill> Bills { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<AppUser>().HasData(
			new AppUser {
				Id = 1,
				Email = "admin@sky-explorer.fr",
				FirstName = "Admin",
				Password = jwtOptions.HashPassword("""p4&nY7]S<m'l3H£59?:^^WG*p&6YPN0wt$L9]gr8"UcjcvE):7"""),
				Role = AppUser.Roles.Admin
			}
		);
	}
}