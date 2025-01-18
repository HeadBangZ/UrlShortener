using Microsoft.AspNetCore.Mvc;
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
	public async Task<string[]> GetAllShortUrlCodes()
	{
		return (await _service.GetAllShortURLCodes()).ToArray();
	}
}
