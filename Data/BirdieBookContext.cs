using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<GolfCourse> GolfCourse { get; set; }

        public DbSet<BirdieBook.Models.TeeBox> TeeBox { get; set; }

        public DbSet<BirdieBook.Models.Hole> Hole { get; set; }

        public DbSet<BirdieBook.Models.UserRound> UserRound { get; set; }

        public DbSet<BirdieBook.Models.UserScore> UserScore { get; set; }
    }
}
