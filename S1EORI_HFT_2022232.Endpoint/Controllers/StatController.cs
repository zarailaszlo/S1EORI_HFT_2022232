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
        IUserLogic userLogic;

        public StatController(IGitRepositoryLogic gitRepositoryLogic, IUserLogic userLogic)
        {
            this.gitRepositoryLogic = gitRepositoryLogic;
            this.userLogic = userLogic;
        }
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
        public int GetCommitCountForRepository([FromQuery] int repositoryId)
        {
            return gitRepositoryLogic.GetCommitCountForRepository(repositoryId);
        }

        [HttpGet]
        public IEnumerable<User> ReadUsersWithZeroRepositories()
        {
            return userLogic.ReadUsersWithZeroRepositories();
        }
        [HttpGet]
        public IEnumerable<User> ReadUsersOlderThan([FromQuery] int age)
        {
            return userLogic.ReadUsersOlderThan(age);
        }

    }
}
