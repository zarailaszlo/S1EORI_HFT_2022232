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
                new User("1#username1#password1#Full Name1#user.email1@example.com#32" ),
                new User("2#username2#password2#Full Name2#user.email2@example.com#46")
            };
            var gitrepositorydata = new GitRepository[]
            {
                new GitRepository ("1#Repository_No.1#private#2023-05-25 10:44:29#2"),
                new GitRepository ("2#Repository_No.2#public#2023-04-11 10:44:29#2")

            };
            var commitdata = new Commit[]
            {
                new Commit("1#1mevilj#Date Iceberg Deleted#2023-05-31 10:44:29#1#2"),
                new Commit("2#24md6qh#Jicama Iceberg Deleted#2023-05-30 10:44:29#1#2"),
                new Commit("3#7jysq4w#Honeydew Honeydew Created#2023-06-03 10:44:29#1#2"),
                new Commit("4#0b33tlj#Elderberry Grape Implemented#2023-05-28 10:44:29#1#2")
            };

            modelBuilder.Entity <User>().HasData(userdata);
            modelBuilder.Entity <GitRepository>().HasData(gitrepositorydata);
            modelBuilder.Entity <Commit>().HasData(commitdata);
            base.OnModelCreating(modelBuilder);
        }
    }
}
