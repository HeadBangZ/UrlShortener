using Microsoft.EntityFrameworkCore;
using ShortenURLOperation.Entities;
using ShortenURLOperation.Services;

namespace ShortenURLOperation.Infrastructure;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<ShortenedUrl> ShortenedUrls => Set<ShortenedUrl>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<ShortenedUrl>(builder =>
		{
			builder.HasIndex(x => new { x.Code }).IsUnique();
			builder.Property(x => x.Code).HasMaxLength(ShortenedUrlService.CodeLength);
		});
	}
}
