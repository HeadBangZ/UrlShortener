using Microsoft.EntityFrameworkCore;
using ShortenURLOperation.Entities;

namespace ShortenURLOperation.Infrastructure;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<ShortenedUrl> ShortenedUrls => Set<ShortenedUrl>();
}
