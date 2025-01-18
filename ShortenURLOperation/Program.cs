
using Microsoft.EntityFrameworkCore;
using ShortenURLOperation.Infrastructure;
using ShortenURLOperation.Services;

namespace ShortenURLOperation
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connString = builder.Configuration.GetConnectionString("AppDbContext");
			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseNpgsql(connString);
			});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddHttpContextAccessor();
			builder.Services.AddScoped<IShortenedUrlService, ShortenedUrlService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
