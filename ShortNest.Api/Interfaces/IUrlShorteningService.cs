namespace ShortNest.Api.Interfaces
{
    public interface IUrlShorteningService
    {
        string ShortenUrl(string originalUrl, TimeSpan expiryDuration);
        string GetOriginalUrl(string shortenedUrl);
    }

}
