using Microsoft.EntityFrameworkCore;
using S1EORI_HFT_2022232.Models;
using System;

namespace S1EORI_HFT_2022232.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GitRepository> GitRepositorys { get; set; }
        public DbSet<Commit> Commits { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public AppDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase("DatabaseGit")
                .UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
