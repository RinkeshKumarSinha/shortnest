namespace ShortNest.Api.Models
{
    public class UrlMapping
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        public TimeSpan ExpiryDuration { get; set; }
    }

}
