using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BirdieBook.Models;

namespace BirdieBook.Data
{
    public class BirdieBookContext : IdentityDbContext<ApplicationUser>
    {
        public BirdieBookContext(DbContextOptions<BirdieBookContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<TagMap>().HasKey(k => new {k.TagId, k.Thing});

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<GolfCourse> GolfCourse { get; set; }

        public DbSet<TeeBox> TeeBox { get; set; }

        public DbSet<Hole> Hole { get; set; }

        public DbSet<UserRound> UserRound { get; set; }

        public DbSet<UserScore> UserScore { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<TagMap> TagMap { get; set; }

    }
}
