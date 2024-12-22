using Microsoft.EntityFrameworkCore;
using ShortNest.Api.Models;

namespace ShortNest.Api
{
    public class ApplicationDbContext : DbContext { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UrlMapping> UrlMappings { get; set; } 
    }
}
