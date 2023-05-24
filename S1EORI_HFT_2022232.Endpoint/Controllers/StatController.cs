using Microsoft.AspNetCore.Mvc;
using S1EORI_HFT_2022232.Logic.Interfaces;
using S1EORI_HFT_2022232.Models;
using S1EORI_HFT_2022232.Models.Statistics;
using S1EORI_HFT_2022232.Repository;
using System;
using System.Collections.Generic;


namespace S1EORI_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGitRepositoryLogic gitRepositoryLogic;
        ICommitLogic commitLogic;
        IUserLogic userLogic;
        public StatController(IGitRepositoryLogic gitRepositoryLogic)
        {
            this.gitRepositoryLogic = gitRepositoryLogic;
        }
        public StatController(ICommitLogic commitLogic)
        {
            this.commitLogic = commitLogic;
        }
        public StatController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        //gitRepositoryLogic non curd
        [HttpGet]
        public IEnumerable<RepositoryStatistics> ReadRepositoryStats()
        {
            return gitRepositoryLogic.ReadRepositoryStats();
        }
        [HttpGet]
        public IEnumerable<VisibilityGroupStatistics> GroupRepositoriesByVisibility()
        {
            return gitRepositoryLogic.GroupRepositoriesByVisibility();
        }
        [HttpGet]
        public int GetCommitCountForRepository(int repositoryId)
        {
            return gitRepositoryLogic.GetCommitCountForRepository(repositoryId);            
        }
        //commitLogic non crud
        [HttpGet("{date}")]
        public IEnumerable<Commit> GetCommitsNewerThan(DateTime date)
        {
            return commitLogic.GetCommitsNewerThan(date);
        }
        [HttpGet("{length}")]
        public IEnumerable<Commit> ReadCommitsLongerThan(int length)
        {
            return commitLogic.ReadCommitsLongerThan(length);
        }
        //userLogic non crud
        [HttpGet]
        public IEnumerable<User> ReadUsersWithZeroRepositories()
        {
            return userLogic.ReadUsersWithZeroRepositories();
        }
        [HttpGet("{email}")]
        public bool DoesUserWithEmailExist(string email)
        {
            return userLogic.DoesUserWithEmailExist(email);
        }
        [HttpGet("{age}")]
        public IEnumerable<User> ReadUsersOlderThan(int age)
        {
            return userLogic.ReadUsersOlderThan(age);
        }
    }
}
