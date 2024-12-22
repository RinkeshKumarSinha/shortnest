using Microsoft.Extensions.Hosting;
using ShortNest.Api;
using System.Threading;
using System.Threading.Tasks;

public class ExpiryService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ExpiryService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var now = DateTime.UtcNow;

                // Fetch all URL mappings and evaluate the expiry condition on the client side
                var expiredUrls = context.UrlMappings
                    .AsEnumerable() // Switch to client-side evaluation
                    .Where(u => now - u.CreatedAt > u.ExpiryDuration)
                    .ToList();

                context.UrlMappings.RemoveRange(expiredUrls);
                await context.SaveChangesAsync();
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Check every minute
        }
    }
}
