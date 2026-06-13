using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Club>()
                .HasMany(c => c.Players)
                .WithOne(p => p.Club)
                .HasForeignKey(p => p.ClubId);

            modelBuilder.Entity<Club>()
                .HasOne(c => c.Coach)
                .WithOne(c => c.Club)
                .HasForeignKey<Coach>(c => c.ClubId);

            modelBuilder.Entity<Club>()
                .HasOne(c => c.Stadium)
                .WithOne(s => s.Club)
                .HasForeignKey<Stadium>(s => s.ClubId);
        }
    }
}