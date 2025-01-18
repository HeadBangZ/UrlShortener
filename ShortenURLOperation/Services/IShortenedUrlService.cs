namespace ShortenURLOperation.Services;

public interface IShortenedUrlService
{
	public string GenerateShortURL(string url);
	public string RetrieveShortURL(string code);
	public Task<IEnumerable<string>> GetAllShortURLCodes();
}
