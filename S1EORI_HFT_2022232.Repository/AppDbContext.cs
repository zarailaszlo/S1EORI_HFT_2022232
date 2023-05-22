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
            modelBuilder.Entity<User>()
                .HasMany(u => u.GitRepositories)
                .WithOne()
                .HasForeignKey(gr => gr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GitRepository>()
                .HasMany(gr => gr.Commits)
                .WithOne(c => c.GitRepository)
                .HasForeignKey(c => c.GitRepositoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Commit>()
                .HasOne(c => c.GitRepository)
                .WithMany(gr => gr.Commits)
                .HasForeignKey(c => c.GitRepositoryId);

            modelBuilder.Entity<Commit>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
