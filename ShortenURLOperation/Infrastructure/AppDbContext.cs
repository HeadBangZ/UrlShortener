using Microsoft.EntityFrameworkCore;
using ShortenURLOperation.Entities;

namespace ShortenURLOperation.Infrastructure;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<ShortenedUrl> ShortenedUrls => Set<ShortenedUrl>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<ShortenedUrl>()
			.HasIndex(x => new { x.Code })
			.IsUnique();

		modelBuilder.Entity<ShortenedUrl>()
			.Property(x => x.Code)
			.HasMaxLength(7);
	}
}
