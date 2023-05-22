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

            var userdata = new User[]
            {
                new User { IdUser = 1, Username = "testuser1", Password = "testpassword1", FullName = "Test User2", Email = "test.user1@example.com", Age = 31 },
                new User { IdUser = 2, Username = "testuser2", Password = "testpassword2", FullName = "Test User2", Email = "test.user2@example.com", Age = 32 }
            };
            var gitrepositorydata = new GitRepository[]
            {
                new GitRepository { IdGitRepository = 1, Name = "testrepo1", Visibility = "public", CreatedDate = DateTime.Now, UserId = 1 },
                new GitRepository { IdGitRepository = 2, Name = "testrepo2", Visibility = "private", CreatedDate = DateTime.Now, UserId = 2 }
            };
            var commitdata = new Commit[]
            {
                new Commit { IdCommit = 1, Hash = "abc123", Message = "Initial commit1", CommittedDate = DateTime.Now, GitRepositoryId = 1, UserId = 1 },
                new Commit { IdCommit = 2, Hash = "def456", Message = "Initial commit2", CommittedDate = DateTime.Now, GitRepositoryId = 1, UserId = 2 }
            };

            modelBuilder.Entity <User>().HasData(userdata);
            modelBuilder.Entity <GitRepository>().HasData(gitrepositorydata);
            modelBuilder.Entity <Commit>().HasData(commitdata);
            base.OnModelCreating(modelBuilder);
        }
    }
}
