using Microsoft.EntityFrameworkCore;
using ShortenURLOperation.Entities;
using ShortenURLOperation.Infrastructure;

namespace ShortenURLOperation.Services;

public class ShortenedUrlService : IShortenedUrlService
{
	private AppDbContext _context;
	private int codeLength = 7;

	public ShortenedUrlService(AppDbContext context)
	{
		_context = context;
	}

	public string GenerateShortURL(string url)
	{
		while (true)
		{
			var code = GenerateCode();

			var result = _context.ShortenedUrls.SingleOrDefault(x => x.Code == code);

			if (result != null) continue;

			var entity = new ShortenedUrl
			{
				Id = Guid.NewGuid(),
				LongUrl = EnsureHttpsUrl(url),
				ShortUrl = $"localhost/{code}",
				Code = code,
				CreatedOnUtc = DateTime.UtcNow,
			};

			_context.ShortenedUrls.Add(entity);
			_context.SaveChanges();

			return entity.ShortUrl;
		}
	}

	public async Task<IEnumerable<string>> GetAllShortURLCodes()
	{
		var codes = await _context.ShortenedUrls.Select(x => x.Code).ToListAsync();
		return codes;
	}

	public async Task<ShortenedUrl?> RetrieveShortURL(string code)
	{
		var shortenedUrl = await _context.ShortenedUrls
																			.Where(x => x.Code == code)
																			.FirstOrDefaultAsync();
		return shortenedUrl;
	}

	private string? GenerateCode()
	{
		string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
		var chars = new char[codeLength];

		for (int i = 0; i < codeLength; i++)
		{
			chars[i] = characters[new Random().Next(characters.Length)];
		}

		return new string(chars);
	}

	private string EnsureHttpsUrl(string url)
	{
		if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
		{
			url = $"https://{url}";
		}

		Uri uri;
		if (Uri.TryCreate(url, UriKind.Absolute, out uri))
		{
			return uri.AbsoluteUri;
		}

		return url;
	}
}
