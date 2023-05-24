using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using S1EORI_HFT_2022232.Repository;
using S1EORI_HFT_2022232.Logic.Classes;
using S1EORI_HFT_2022232.Models;

namespace S1EORI_HFT_2022232.Test
{
    [TestFixture]
    public class Tester
    {
        CommitLogic CommitLogic;
        GitRepositoryLogic GitRepositoryLogic;
        UserLogic UserLogic;
        Mock<IRepository<Commit>> CommitMock;
        Mock<IRepository<GitRepository>> GitRepositoryMock;
        Mock<IRepository<User>> UserMock;
        Commit commit1;
        Commit commit2;
        GitRepository gitRepository1;
        GitRepository gitRepository2;
        User user1;
        User user2;


        [SetUp]
        public void Init()
        {
            CommitMock = new Mock<IRepository<Commit>>();
            GitRepositoryMock = new Mock<IRepository<GitRepository>>();
            UserMock = new Mock<IRepository<User>>();

            user1 = new User();
            user1.IdUser = 1;
            user1.Username = "JohnDoe";
            user1.Password = "SecurePassword";
            user1.FullName = "John Doe";
            user1.Email = "johndoe@example.com";
            user1.Age = 35;

            user2 = new User();
            user2.IdUser = 2;
            user2.Username = "JaneDoe";
            user2.Password = "SecurePassword2";
            user2.FullName = "Jane Doe";
            user2.Email = "janedoe@example.com";
            user2.Age = 30;

            gitRepository1 = new GitRepository();
            gitRepository1.IdGitRepository = 1;
            gitRepository1.Name = "JohnsRepository";
            gitRepository1.Visibility = "public";
            gitRepository1.CreatedDate = DateTime.Now;
            gitRepository1.UserId = user1.IdUser;
            gitRepository1.User = user1;

            gitRepository2 = new GitRepository();
            gitRepository2.IdGitRepository = 2;
            gitRepository2.Name = "JanesRepository";
            gitRepository2.Visibility = "private";
            gitRepository2.CreatedDate = DateTime.Now;
            gitRepository2.UserId = user2.IdUser;
            gitRepository2.User = user2;

            commit1 = new Commit();
            commit1.IdCommit = 1;
            commit1.Hash = "abc123";
            commit1.Message = "Initial commit in John's repository";
            commit1.CommittedDate = DateTime.Now;
            commit1.GitRepositoryId = gitRepository1.IdGitRepository;
            commit1.UserId = user1.IdUser;
            commit1.User = user1;
            commit1.GitRepository = gitRepository1;

            commit2 = new Commit();
            commit2.IdCommit = 2;
            commit2.Hash = "def456";
            commit2.Message = "Initial commit in Jane's repository";
            commit2.CommittedDate = DateTime.Now;
            commit2.GitRepositoryId = gitRepository2.IdGitRepository;
            commit2.UserId = user2.IdUser;
            commit2.User = user2;
            commit2.GitRepository = gitRepository2;

            CommitMock.Setup(repo => repo.Read(1)).Returns(commit1);
            CommitMock.Setup(repo => repo.Read(2)).Returns(commit2);

            GitRepositoryMock.Setup(repo => repo.Read(1)).Returns(gitRepository1);
            GitRepositoryMock.Setup(repo => repo.Read(2)).Returns(gitRepository2);

            UserMock.Setup(repo => repo.Read(1)).Returns(user1);
            UserMock.Setup(repo => repo.Read(2)).Returns(user2);

            List<Commit> commits = new List<Commit>() { commit1, commit2 };
            CommitMock.Setup(repo => repo.ReadAll()).Returns(commits.AsQueryable());

            gitRepository1.Commits = new List<Commit>() { commit1 };
            gitRepository2.Commits = new List<Commit>() { commit2 };

            List<GitRepository> repos = new List<GitRepository>() { gitRepository1, gitRepository2 };
            GitRepositoryMock.Setup(repo => repo.ReadAll()).Returns(repos.AsQueryable());

            user2.GitRepositories = new List<GitRepository>() { gitRepository1 };
            user2.GitRepositories = new List<GitRepository>() { gitRepository2 };

            List<User> users = new List<User>() { user1, user2 };
            UserMock.Setup(repo => repo.ReadAll()).Returns(users.AsQueryable());

            

            CommitLogic = new CommitLogic(CommitMock.Object);
            GitRepositoryLogic = new GitRepositoryLogic(GitRepositoryMock.Object);
            UserLogic = new UserLogic(UserMock.Object);
        }
        //read test
        [Test]
        public void ReadCommit_Test()
        {
            var result = CommitLogic.Read(1);
            Assert.That(result, Is.EqualTo(commit1));
        }

        [Test]
        public void ReadUser_Test()
        {
            var result = UserLogic.Read(1);
            Assert.That(result, Is.EqualTo(user1));
        }
        [Test]
        public void ReadGitRepository_Test()
        {
            var result = GitRepositoryLogic.Read(1);
            Assert.That(result, Is.EqualTo(gitRepository1));
        }
        //readALL test
        [Test]
        public void ReadAllCommits_Test()
        {
            var result = CommitLogic.ReadAll().ToList();
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Has.Member(commit1));
            Assert.That(result, Has.Member(commit2));
        }


        [Test]
        public void ReadAllGitRepositories_Test()
        {
            var result = GitRepositoryLogic.ReadAll().ToList();
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Has.Member(gitRepository1));
            Assert.That(result, Has.Member(gitRepository2));
        }


        [Test]
        public void ReadAllUsers_Test()
        {
            var result = UserLogic.ReadAll().ToList();
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Has.Member(user1));
            Assert.That(result, Has.Member(user2));
        }

        //Creat [Test]
        public void CreateUser_Test()
        {
            // Create user with zero repositories
            User newUser = new User();
            newUser.IdUser = 3;
            newUser.Username = "NewUser";
            newUser.Password = "NewPassword";
            newUser.FullName = "New User";
            newUser.Email = "newuser@example.com";
            newUser.Age = 25;

            UserMock.Setup(repo => repo.Create(newUser));

            UserLogic.Create(newUser);

            UserMock.Verify(repo => repo.Create(newUser), Times.Once);
        }

        [Test]
        public void CreateGitRepository_Test()
        {
            GitRepository newRepo = new GitRepository();
            newRepo.IdGitRepository = 3;
            newRepo.Name = "NewRepository";
            newRepo.Visibility = "private";
            newRepo.CreatedDate = DateTime.Now;
            newRepo.UserId = user1.IdUser;
            newRepo.User = user1;

            GitRepositoryMock.Setup(repo => repo.Create(newRepo));

            GitRepositoryLogic.Create(newRepo);

            GitRepositoryMock.Verify(repo => repo.Create(newRepo), Times.Once);
        }
        [Test]
        public void CreateCommit_WithTooShortHash_Test()
        {
            Commit newCommit = new Commit();
            newCommit.IdCommit = 3;
            newCommit.Hash = "ghi"; // This hash is too short
            newCommit.Message = "New commit in NewRepository";
            newCommit.CommittedDate = DateTime.Now;
            newCommit.GitRepositoryId = gitRepository1.IdGitRepository;
            newCommit.UserId = user1.IdUser;
            newCommit.User = user1;
            newCommit.GitRepository = gitRepository1;

            CommitMock.Setup(repo => repo.Create(newCommit)).Throws(new ArgumentException("Hash is too short (minimum 7 character)"));
            var exception = Assert.Throws<ArgumentException>(() => CommitLogic.Create(newCommit));
            Assert.AreEqual("Hash is too short (minimum 7 character)", exception.Message);
        }
        [Test]
        public void CreateCommit_Test()
        {
            Commit newCommit = new Commit();
            newCommit.IdCommit = 3;
            newCommit.Hash = "ghi75489";
            newCommit.Message = "New commit in NewRepository";
            newCommit.CommittedDate = DateTime.Now;
            newCommit.GitRepositoryId = gitRepository1.IdGitRepository;
            newCommit.UserId = user1.IdUser;
            newCommit.User = user1;
            newCommit.GitRepository = gitRepository1;

            CommitMock.Setup(repo => repo.Create(newCommit));
            CommitLogic.Create(newCommit);
            CommitMock.Verify(repo => repo.Create(newCommit), Times.Once);
        }
        //Non-CRUD user
        [Test]
        public void ReadUsersWithZeroRepositories_Test()
        {
            // Create user with zero repositories
            User userZeroRepo = new User();
            userZeroRepo.IdUser = 3;
            userZeroRepo.Username = "ZeroRepoUser";
            userZeroRepo.Password = "ZeroRepoPassword";
            userZeroRepo.FullName = "Zero Repo User";
            userZeroRepo.Email = "zerorepo@example.com";
            userZeroRepo.Age = 40;
            userZeroRepo.GitRepositories = new List<GitRepository>();

            UserMock.Setup(repo => repo.ReadAll()).Returns((new List<User>() { user1, user2, userZeroRepo }).AsQueryable());
            UserLogic = new UserLogic(UserMock.Object);

            var result = UserLogic.ReadUsersWithZeroRepositories().ToList();
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Has.Member(userZeroRepo));
        }

        [Test]
        public void ReadUsersOlderThan_Test()
        {
            var result = UserLogic.ReadUsersOlderThan(34).ToList();
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result, Has.Member(user1));
        }
        //Non-CRUD Repository
        [Test]
        public void ReadRepositoryStats_Test()
        {
            var result = GitRepositoryLogic.ReadRepositoryStats().ToList();

            Assert.That(result, Has.Count.EqualTo(2));

            var repo1Stats = result.FirstOrDefault(s => s.RepositoryName == "JohnsRepository");
            Assert.IsNotNull(repo1Stats);
            Assert.That(repo1Stats.UserId, Is.EqualTo("1"));
            Assert.That(repo1Stats.CommitCount, Is.EqualTo(1));
            Assert.That(repo1Stats.AverageCommitLength, Is.EqualTo("Initial commit in John's repository".Length));

            var repo2Stats = result.FirstOrDefault(s => s.RepositoryName == "JanesRepository");
            Assert.IsNotNull(repo2Stats);
            Assert.That(repo2Stats.UserId, Is.EqualTo("2"));
            Assert.That(repo2Stats.CommitCount, Is.EqualTo(1));
            Assert.That(repo2Stats.AverageCommitLength, Is.EqualTo("Initial commit in Jane's repository".Length));
        }
        [Test]
        public void GroupRepositoriesByVisibility_Test()
        {
            var result = GitRepositoryLogic.GroupRepositoriesByVisibility().ToList();
            Assert.That(result, Has.Count.EqualTo(2));

            var publicGroup = result.FirstOrDefault(s => s.Visibility == "public");
            Assert.IsNotNull(publicGroup);
            Assert.That(publicGroup.RepositoryCount, Is.EqualTo(1));

            var privateGroup = result.FirstOrDefault(s => s.Visibility == "private");
            Assert.IsNotNull(privateGroup);
            Assert.That(privateGroup.RepositoryCount, Is.EqualTo(1));
        }
    }
}
