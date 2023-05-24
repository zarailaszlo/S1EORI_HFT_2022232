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

            // Create first User
            user1 = new User();
            user1.IdUser = 1;
            user1.Username = "JohnDoe";
            user1.Password = "SecurePassword";
            user1.FullName = "John Doe";
            user1.Email = "johndoe@example.com";
            user1.Age = 35;

            // Create second User
            user2 = new User();
            user2.IdUser = 2;
            user2.Username = "JaneDoe";
            user2.Password = "SecurePassword2";
            user2.FullName = "Jane Doe";
            user2.Email = "janedoe@example.com";
            user2.Age = 30;

            // Create first GitRepository
            gitRepository1 = new GitRepository();
            gitRepository1.IdGitRepository = 1;
            gitRepository1.Name = "JohnsRepository";
            gitRepository1.Visibility = "Public";
            gitRepository1.CreatedDate = DateTime.Now;
            gitRepository1.UserId = user1.IdUser;
            gitRepository1.User = user1;

            // Create second GitRepository
            gitRepository2 = new GitRepository();
            gitRepository2.IdGitRepository = 2;
            gitRepository2.Name = "JanesRepository";
            gitRepository2.Visibility = "Private";
            gitRepository2.CreatedDate = DateTime.Now;
            gitRepository2.UserId = user2.IdUser;
            gitRepository2.User = user2;

            // Create first Commit
            commit1 = new Commit();
            commit1.IdCommit = 1;
            commit1.Hash = "abc123";
            commit1.Message = "Initial commit in John's repository";
            commit1.CommittedDate = DateTime.Now;
            commit1.GitRepositoryId = gitRepository1.IdGitRepository;
            commit1.UserId = user1.IdUser;
            commit1.User = user1;
            commit1.GitRepository = gitRepository1;

            // Create second Commit
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

            List<GitRepository> repos = new List<GitRepository>() { gitRepository1, gitRepository1 };
            GitRepositoryMock.Setup(repo => repo.ReadAll()).Returns(repos.AsQueryable());

            List<User> users = new List<User>() { user1, user2 };
            UserMock.Setup(repo => repo.ReadAll()).Returns(users.AsQueryable());

            // Logic classes initialization
            CommitLogic = new CommitLogic(CommitMock.Object);
            GitRepositoryLogic = new GitRepositoryLogic(GitRepositoryMock.Object);
            UserLogic = new UserLogic(UserMock.Object);
        }


    }
}
