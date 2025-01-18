using ShortenURLOperation.Entities;

namespace ShortenURLOperation.Services;

public interface IShortenedUrlService
{
	public string GenerateShortURL(string url);
	public Task<ShortenedUrl?> RetrieveShortURL(string code);
	public Task<IEnumerable<string>> GetAllShortURLCodes();
}
