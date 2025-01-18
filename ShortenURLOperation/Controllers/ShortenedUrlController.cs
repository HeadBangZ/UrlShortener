using Microsoft.AspNetCore.Mvc;
using ShortenURLOperation.Entities;
using ShortenURLOperation.Requests;
using ShortenURLOperation.Services;

namespace ShortenURLOperation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortenedUrlController : ControllerBase
{
	private IShortenedUrlService _service;
	private HttpContext _httpContext;

	public ShortenedUrlController(IShortenedUrlService service, IHttpContextAccessor httpContextAccessor)
	{
		_service = service;

		if (httpContextAccessor.HttpContext == null)
		{
			throw new ArgumentNullException(nameof(httpContextAccessor));
		}
		_httpContext = httpContextAccessor.HttpContext;
	}

	[HttpGet]
	public async Task<ActionResult<string[]>> GetAllShortUrls()
	{
		return (await _service.GetAllShortURLs()).ToArray();
	}

	[HttpGet("{code}")]
	public async Task<ActionResult<ShortenedUrl>> GetShortenedUrlObject(string code)
	{
		var shortenedUrl = await _service.RetrieveShortURL(code);

		if (shortenedUrl == null)
		{
			return NotFound();
		}

		return Ok(shortenedUrl);
	}

	[HttpPost]
	public async Task<ActionResult> GenerateShortUrl(ShortenedUrlRequest request)
	{
		if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
		{
			return BadRequest("The specified URL is invalid");
		}

		var shortUrl = await _service.GenerateShortURL(request, _httpContext);

		return Ok(shortUrl);
	}
}
