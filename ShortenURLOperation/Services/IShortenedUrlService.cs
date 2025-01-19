using ShortenURLOperation.Entities;
using ShortenURLOperation.Requests;

namespace ShortenURLOperation.Services;

public interface IShortenedUrlService
{
	public Task<string> GenerateShortURL(ShortenedUrlRequest request, HttpContext httpContext);
	public Task<ShortenedUrl?> RetrieveShortenedURL(string code);
	public Task<IEnumerable<string>> GetAllShortURLCodess();
}
