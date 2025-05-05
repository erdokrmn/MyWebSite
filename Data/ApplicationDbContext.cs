using Microsoft.EntityFrameworkCore;
using MyWebSite.Models;
using System.Collections.Generic;

namespace MyWebSite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SiteContent> SiteContents { get; set; }

        public DbSet<SocialLink> SocialLinks { get; set; }
    }
}
