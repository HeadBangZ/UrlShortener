using Microsoft.AspNetCore.Mvc;
using ShortenURLOperation.Entities;
using ShortenURLOperation.Services;

namespace ShortenURLOperation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortenedUrlController : ControllerBase
{
	private IShortenedUrlService _service;

	public ShortenedUrlController(IShortenedUrlService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<ActionResult<string[]>> GetAllShortUrlCodes()
	{
		return (await _service.GetAllShortURLCodes()).ToArray();
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
}
