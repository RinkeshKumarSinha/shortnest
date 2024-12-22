using Microsoft.AspNetCore.Mvc;
using ShortNest.Api.Interfaces;

namespace ShortNest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly IUrlShorteningService _urlShorteningService;

        public MainController(IUrlShorteningService urlShorteningService)
        {
            _urlShorteningService = urlShorteningService;
        }

        [HttpPost("shorten")]
        public IActionResult PostUri([FromBody] UrlRequest request)
        {
            var shortenedUrl = _urlShorteningService.ShortenUrl(request.Url, request.ExpiryDuration);
            return Ok("https://shortNest.com/"+shortenedUrl);
        }

        [HttpGet("{shortenedUrl}")]
        public IActionResult GetOriginalUrl(string shortenedUrl)
        {

            shortenedUrl = Uri.UnescapeDataString(shortenedUrl);
            string fullUrl = shortenedUrl;
            string baseUrl = "https://shortNest.com/";
            string shortUrl = fullUrl;

            if (fullUrl.StartsWith(baseUrl))
            {
                shortUrl = fullUrl.Substring(baseUrl.Length);
            }

        


            var originalUrl = _urlShorteningService.GetOriginalUrl(shortUrl);
            if (originalUrl == null)
            {
                return NotFound();
            }
            return Ok(originalUrl);
        }
    }

    public class UrlRequest
    {
        public string Url { get; set; }
        public TimeSpan ExpiryDuration { get; set; }
    }
}
