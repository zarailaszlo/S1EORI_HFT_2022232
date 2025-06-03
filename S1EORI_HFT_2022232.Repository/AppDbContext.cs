using Microsoft.EntityFrameworkCore;
using S1EORI_HFT_2022232.Models;
using System;

namespace S1EORI_HFT_2022232.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GitRepository> GitRepositories { get; set; }
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


            modelBuilder.Entity<User>(user =>
            {
                user.HasMany(u => u.GitRepositories)
                    .WithOne(gr => gr.User)
                    .HasForeignKey(gr => gr.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(u => u.Commits)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GitRepository>(gitRepo =>
            {
                gitRepo.HasOne(gr => gr.User)
                    .WithMany(u => u.GitRepositories)
                    .HasForeignKey(gr => gr.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                gitRepo.HasMany(gr => gr.Commits)
                    .WithOne(c => c.GitRepository)
                    .HasForeignKey(c => c.GitRepositoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Commit>(commit =>
            {
                commit.HasOne(c => c.User)
                    .WithMany(u => u.Commits)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                commit.HasOne(c => c.GitRepository)
                    .WithMany(gr => gr.Commits)
                    .HasForeignKey(c => c.GitRepositoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            var userdata = new User[]
            {
                new User("1#username1#password1#Full Name1#user.email1@example.com#42"),
                new User("2#username2#password2#Full Name2#user.email2@example.com#32"),
                new User("3#username3#password3#Full Name3#user.email3@example.com#53"),
                new User("4#username4#password4#Full Name4#user.email4@example.com#36"),
                new User("5#username5#password5#Full Name5#user.email5@example.com#39"),
                new User("6#username6#password6#Full Name6#user.email6@example.com#31"),
                new User("7#username7#password7#Full Name7#user.email7@example.com#38"),
                new User("8#username8#password8#Full Name8#user.email8@example.com#47"),
                new User("9#username9#password9#Full Name9#user.email9@example.com#58"),
                new User("10#username10#password10#Full Name10#user.email10@example.com#33"),
                new User("11#username11#password11#Full Name11#user.email11@example.com#13"),
                new User("12#username12#password12#Full Name12#user.email12@example.com#27"),
                new User("13#username13#password13#Full Name13#user.email13@example.com#41")
            };
            var gitrepositorydata = new GitRepository[]
            {
                new GitRepository ("1#Repository_No.1#private#2023-03-13 12:29:47#2"),
                new GitRepository ("2#Repository_No.2#public#2023-03-16 12:29:47#3"),
                new GitRepository ("3#Repository_No.3#private#2023-04-25 12:29:47#4"),
                new GitRepository ("4#Repository_No.4#public#2023-03-18 12:29:47#4"),
                new GitRepository ("5#Repository_No.5#private#2023-05-25 12:29:47#5"),
                new GitRepository ("6#Repository_No.6#private#2023-04-28 12:29:47#6"),
                new GitRepository ("7#Repository_No.7#public#2023-05-08 12:29:47#7"),
                new GitRepository ("8#Repository_No.8#private#2023-05-22 12:29:47#7"),
                new GitRepository ("9#Repository_No.9#public#2023-03-24 12:29:47#8"),
                new GitRepository ("10#Repository_No.10#private#2023-03-05 12:29:47#8"),
                new GitRepository ("11#Repository_No.11#private#2023-05-01 12:29:47#9"),
                new GitRepository ("12#Repository_No.12#public#2023-05-19 12:29:47#9"),
                new GitRepository ("13#Repository_No.13#public#2023-02-23 12:29:47#9"),
                new GitRepository ("14#Repository_No.14#private#2023-03-08 12:29:47#11"),
                new GitRepository ("15#Repository_No.15#private#2023-04-07 12:29:47#11"),
                new GitRepository ("16#Repository_No.16#public#2023-06-08 12:29:47#12"),
                new GitRepository ("17#Repository_No.17#private#2023-03-10 12:29:47#12"),
                new GitRepository ("18#Repository_No.18#private#2023-04-22 12:29:47#12"),
                new GitRepository ("19#Repository_No.19#private#2023-02-24 12:29:47#13"),
                new GitRepository ("20#Repository_No.20#private#2023-03-16 12:29:47#13")

            };
            var commitdata = new Commit[]
            {
                new Commit("1#8zsrv4f#Cherry Date Refactored#2023-03-21 12:29:47#1#2"),
                new Commit("2#0hxia1f#Cherry Apple Refactored#2023-03-19 12:29:47#1#2"),
                new Commit("3#19itfm0#Elderberry Elderberry Added#2023-03-15 12:29:47#1#2"),
                new Commit("4#0kb56m3#Grape Honeydew Added#2023-03-18 12:29:47#1#2"),
                new Commit("5#1atcoon#Fig Iceberg Implemented#2023-03-15 12:29:47#1#2"),
                new Commit("6#2utibz3#Elderberry Jicama Fixed#2023-03-19 12:29:47#1#2"),
                new Commit("7#8qq3pw2#Cherry Jicama Added#2023-03-25 12:29:47#2#3"),
                new Commit("8#3b5kw89#Elderberry Date Refactored#2023-03-19 12:29:47#2#3"),
                new Commit("9#14ljktf#Elderberry Grape Fixed#2023-03-19 12:29:47#2#3"),
                new Commit("10#50pek57#Honeydew Apple Created#2023-03-22 12:29:47#2#3"),
                new Commit("11#8e4nmig#Date Grape Tested#2023-03-18 12:29:47#2#3"),
                new Commit("12#2duo0ct#Banana Iceberg Implemented#2023-05-01 12:29:47#3#4"),
                new Commit("13#1uwxjs9#Banana Elderberry Improved#2023-04-27 12:29:47#3#4"),
                new Commit("14#9ilsrki#Apple Fig Tested#2023-05-02 12:29:47#3#4"),
                new Commit("15#5r6rgec#Date Cherry Deleted#2023-05-02 12:29:47#3#4"),
                new Commit("16#5iefti3#Grape Date Tested#2023-04-29 12:29:47#3#4"),
                new Commit("17#49f02m5#Banana Banana Refactored#2023-06-01 12:29:47#5#5"),
                new Commit("18#1zcom0a#Cherry Iceberg Deleted#2023-06-03 12:29:47#5#5"),
                new Commit("19#1qzfc6r#Apple Date Tested#2023-06-01 12:29:47#5#5"),
                new Commit("20#63fa3qq#Honeydew Cherry Refactored#2023-06-01 12:29:47#5#5"),
                new Commit("21#3ava3is#Iceberg Elderberry Added#2023-05-29 12:29:47#5#5"),
                new Commit("22#43yaxk8#Grape Apple Fixed#2023-06-03 12:29:47#5#5"),
                new Commit("23#4bjim7t#Iceberg Banana Fixed#2023-05-28 12:29:47#5#5"),
                new Commit("24#3l23g2v#Grape Elderberry Updated#2023-05-14 12:29:47#7#7"),
                new Commit("25#51v5uly#Jicama Banana Fixed#2023-05-12 12:29:47#7#7"),
                new Commit("26#5ldi5ms#Iceberg Honeydew Debugged#2023-05-15 12:29:47#7#7"),
                new Commit("27#96bf0wl#Grape Grape Improved#2023-05-15 12:29:47#7#7"),
                new Commit("28#19kro9f#Fig Grape Added#2023-05-09 12:29:47#7#7"),
                new Commit("29#97jdxx1#Honeydew Honeydew Tested#2023-05-30 12:29:47#8#7"),
                new Commit("30#4ucxssr#Iceberg Cherry Added#2023-05-31 12:29:47#8#7"),
                new Commit("31#48sxfgx#Iceberg Jicama Fixed#2023-05-29 12:29:47#8#7"),
                new Commit("32#0xonvjd#Date Banana Deleted#2023-05-30 12:29:47#8#7"),
                new Commit("33#5uur9vu#Elderberry Jicama Fixed#2023-05-28 12:29:47#8#7"),
                new Commit("34#0sluarv#Banana Jicama Tested#2023-05-28 12:29:47#8#7"),
                new Commit("35#2z57pwo#Jicama Apple Refactored#2023-03-26 12:29:47#9#8"),
                new Commit("36#0ktnqqw#Iceberg Apple Created#2023-03-27 12:29:47#9#8"),
                new Commit("37#61bub93#Apple Apple Tested#2023-03-29 12:29:47#9#8"),
                new Commit("38#6hhnxv6#Jicama Grape Improved#2023-04-01 12:29:47#9#8"),
                new Commit("39#4dxggjd#Honeydew Date Created#2023-04-02 12:29:47#9#8"),
                new Commit("40#8upeawp#Fig Grape Updated#2023-03-07 12:29:47#10#8"),
                new Commit("41#02b9gok#Iceberg Date Improved#2023-03-14 12:29:47#10#8"),
                new Commit("42#82u7jnn#Honeydew Cherry Added#2023-03-08 12:29:47#10#8"),
                new Commit("43#7zlrr7g#Jicama Fig Created#2023-03-13 12:29:47#10#8"),
                new Commit("44#0gk0n6t#Honeydew Cherry Tested#2023-03-14 12:29:47#10#8"),
                new Commit("45#0h7ea3b#Date Grape Refactored#2023-05-10 12:29:47#11#9"),
                new Commit("46#3woq6b1#Date Grape Implemented#2023-05-02 12:29:47#11#9"),
                new Commit("47#4gs5t40#Grape Iceberg Improved#2023-05-09 12:29:47#11#9"),
                new Commit("48#86lczan#Cherry Date Refactored#2023-05-03 12:29:47#11#9"),
                new Commit("49#6nwmrcy#Iceberg Banana Deleted#2023-05-09 12:29:47#11#9"),
                new Commit("50#1khvb49#Elderberry Honeydew Implemented#2023-05-21 12:29:47#12#9"),
                new Commit("51#8l0anfl#Cherry Fig Added#2023-05-22 12:29:47#12#9"),
                new Commit("52#1my790o#Apple Iceberg Implemented#2023-05-22 12:29:47#12#9"),
                new Commit("53#14cc2xn#Grape Iceberg Debugged#2023-05-27 12:29:47#12#9"),
                new Commit("54#3imefcx#Honeydew Elderberry Fixed#2023-05-24 12:29:47#12#9"),
                new Commit("55#9c46e4i#Apple Honeydew Refactored#2023-05-24 12:29:47#12#9"),
                new Commit("56#5h58lmy#Iceberg Jicama Fixed#2023-05-23 12:29:47#12#9"),
                new Commit("57#8vmoefd#Grape Jicama Improved#2023-02-25 12:29:47#13#9"),
                new Commit("58#8tto3dg#Iceberg Honeydew Added#2023-02-26 12:29:47#13#9"),
                new Commit("59#20kbulg#Fig Honeydew Debugged#2023-02-28 12:29:47#13#9"),
                new Commit("60#59xzmsn#Elderberry Fig Improved#2023-03-04 12:29:47#13#9"),
                new Commit("61#1qfmspl#Banana Banana Tested#2023-02-28 12:29:47#13#9"),
                new Commit("62#6ovddu1#Elderberry Date Fixed#2023-02-27 12:29:47#13#9"),
                new Commit("63#4kukjx4#Iceberg Cherry Debugged#2023-03-15 12:29:47#14#11"),
                new Commit("64#2tx0bni#Banana Cherry Deleted#2023-03-16 12:29:47#14#11"),
                new Commit("65#6dux0af#Iceberg Honeydew Updated#2023-03-14 12:29:47#14#11"),
                new Commit("66#41p6mf8#Cherry Cherry Updated#2023-03-17 12:29:47#14#11"),
                new Commit("67#7g0zz8f#Fig Apple Deleted#2023-03-11 12:29:47#14#11"),
                new Commit("68#6wqzv64#Honeydew Date Created#2023-03-12 12:29:47#14#11"),
                new Commit("69#6b6kfbd#Banana Apple Added#2023-03-12 12:29:47#14#11"),
                new Commit("70#0kgyile#Cherry Banana Debugged#2023-04-11 12:29:47#15#11"),
                new Commit("71#6tbpq85#Date Fig Tested#2023-04-15 12:29:47#15#11"),
                new Commit("72#43dp4qc#Banana Jicama Added#2023-04-10 12:29:47#15#11"),
                new Commit("73#66tu04c#Fig Iceberg Tested#2023-04-12 12:29:47#15#11"),
                new Commit("74#7ptdv0q#Iceberg Apple Debugged#2023-04-09 12:29:47#15#11"),
                new Commit("75#14mu2rx#Banana Jicama Debugged#2023-04-11 12:29:47#15#11"),
                new Commit("76#0i96c8e#Grape Iceberg Tested#2023-06-16 12:29:47#16#12"),
                new Commit("77#3zdcprz#Grape Cherry Added#2023-06-09 12:29:47#16#12"),
                new Commit("78#65x3und#Grape Jicama Tested#2023-06-16 12:29:47#16#12"),
                new Commit("79#2325le5#Cherry Iceberg Improved#2023-06-15 12:29:47#16#12"),
                new Commit("80#73h3j5n#Elderberry Banana Refactored#2023-06-09 12:29:47#16#12"),
                new Commit("81#9xeiio0#Fig Banana Debugged#2023-06-10 12:29:47#16#12"),
                new Commit("82#4rjjugv#Iceberg Jicama Fixed#2023-06-10 12:29:47#16#12"),
                new Commit("83#7dikssf#Banana Iceberg Added#2023-03-17 12:29:47#17#12"),
                new Commit("84#05rsk16#Fig Date Tested#2023-03-15 12:29:47#17#12"),
                new Commit("85#0kh4nz2#Iceberg Honeydew Improved#2023-03-14 12:29:47#17#12"),
                new Commit("86#6wxm5lx#Date Date Deleted#2023-03-12 12:29:47#17#12"),
                new Commit("87#8dzlqam#Jicama Fig Implemented#2023-03-18 12:29:47#17#12"),
                new Commit("88#2d8dxeq#Date Jicama Updated#2023-03-17 12:29:47#17#12"),
                new Commit("89#7i4585u#Jicama Jicama Updated#2023-03-11 12:29:47#17#12"),
                new Commit("90#0i2s1p3#Honeydew Elderberry Implemented#2023-04-26 12:29:47#18#12"),
                new Commit("91#7xt59q0#Apple Date Deleted#2023-04-27 12:29:47#18#12"),
                new Commit("92#6brpwus#Banana Cherry Deleted#2023-05-01 12:29:47#18#12"),
                new Commit("93#5bhvrwm#Iceberg Elderberry Deleted#2023-04-25 12:29:47#18#12"),
                new Commit("94#5c6ndnx#Banana Elderberry Tested#2023-04-28 12:29:47#18#12"),
                new Commit("95#2ikl73e#Elderberry Grape Deleted#2023-03-05 12:29:47#19#13"),
                new Commit("96#1zr8akr#Elderberry Fig Fixed#2023-03-01 12:29:47#19#13"),
                new Commit("97#1qcai5p#Fig Iceberg Deleted#2023-02-27 12:29:47#19#13"),
                new Commit("98#5xje9xj#Honeydew Jicama Deleted#2023-03-05 12:29:47#19#13"),
                new Commit("99#6b1jyvm#Cherry Fig Debugged#2023-03-02 12:29:47#19#13"),
                new Commit("100#7d4j7vj#Apple Apple Implemented#2023-03-01 12:29:47#19#13"),
                new Commit("101#8k4f0mc#Banana Honeydew Created#2023-03-19 12:29:47#20#13"),
                new Commit("102#9g4iv5p#Jicama Iceberg Added#2023-03-19 12:29:47#20#13"),
                new Commit("103#7zys5mp#Banana Banana Implemented#2023-03-20 12:29:47#20#13"),
                new Commit("104#9zbpqdi#Honeydew Jicama Created#2023-03-19 12:29:47#20#13"),
                new Commit("105#6wcg48x#Elderberry Cherry Improved#2023-03-21 12:29:47#20#13"),
                new Commit("106#6ueqhr0#Honeydew Fig Debugged#2023-03-23 12:29:47#20#13"),
                new Commit("107#5hfgfol#Honeydew Banana Fixed#2023-03-18 12:29:47#20#13")
            };

            modelBuilder.Entity <User>().HasData(userdata);
            modelBuilder.Entity <GitRepository>().HasData(gitrepositorydata);
            modelBuilder.Entity <Commit>().HasData(commitdata);
            base.OnModelCreating(modelBuilder);
        }
    }
}
