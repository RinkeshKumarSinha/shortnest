using ShortNest.Api.Encoders;
using ShortNest.Api.Interfaces;
using ShortNest.Api.Models;

namespace ShortNest.Api.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        private readonly ApplicationDbContext _context;

        public UrlShorteningService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string ShortenUrl(string originalUrl, TimeSpan expiryDuration)
        {
            var id = _context.UrlMappings.Count() + 1; // Simple way to generate a unique ID
            var shortenedUrl = Base62Encoder.Encode(id);
            var urlMapping = new UrlMapping
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrl,
                CreatedAt = DateTime.UtcNow,
                ExpiryDuration = expiryDuration
            };
            _context.UrlMappings.Add(urlMapping);
            _context.SaveChanges();
            return shortenedUrl;
        }

        public string GetOriginalUrl(string shortenedUrl)
        {
            var urlMapping = _context.UrlMappings.FirstOrDefault(u => u.ShortenedUrl == shortenedUrl);
            return urlMapping?.OriginalUrl;
        }
    }



}
