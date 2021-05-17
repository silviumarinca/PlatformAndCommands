using Microsoft.EntityFrameworkCore;
using CommanderGQL.Models;

namespace CommanderGQL.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>()
            .HasMany(c => c.Commands)
            .WithOne(p => p.Platform!)
            .HasForeignKey(p=>p.PlatformId);
            modelBuilder.Entity<Command>()
            .HasOne(p=>p.Platform)
            .WithMany(c=>c.Commands)
            .HasForeignKey(p => p.PlatformId);

        }
    }
}