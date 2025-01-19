using Microsoft.EntityFrameworkCore;
using ShortenURLOperation.Entities;
using ShortenURLOperation.Infrastructure;
using ShortenURLOperation.Requests;

namespace ShortenURLOperation.Services;

public class ShortenedUrlService : IShortenedUrlService
{
	public const int CodeLength = 7;

	private AppDbContext _context;
	private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

	private readonly Random _random = new Random();


	public ShortenedUrlService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<string> GenerateShortURL(ShortenedUrlRequest request, HttpContext httpContext)
	{
		var code = await GenerateCode();

		var entity = new ShortenedUrl
		{
			Id = Guid.NewGuid(),
			LongUrl = request.Url,
			ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}",
			Code = code,
			CreatedOnUtc = DateTime.UtcNow,
		};

		_context.ShortenedUrls.Add(entity);
		_context.SaveChanges();

		return entity.ShortUrl;
	}

	public async Task<IEnumerable<string>> GetAllShortURLCodess()
	{
		var codes = await _context.ShortenedUrls.Select(x => x.Code).ToListAsync();
		return codes;
	}

	public async Task<ShortenedUrl?> RetrieveShortenedURL(string code)
	{
		var shortenedUrl = await _context.ShortenedUrls
																			.Where(x => x.Code == code)
																			.FirstOrDefaultAsync();
		return shortenedUrl;
	}

	private async Task<string> GenerateCode()
	{
		var codeChars = new char[CodeLength];

		while (true)
		{
			for (int i = 0; i < CodeLength; i++)
			{
				var idx = _random.Next(Characters.Length);
				codeChars[i] = Characters[idx];
			}

			var code = new string(codeChars);

			if (!await _context.ShortenedUrls.AnyAsync(x => x.Code == code))
			{
				return code;
			}
		}
	}
}
