using Microsoft.AspNetCore.Mvc;
using S1EORI_HFT_2022232.Logic.Interfaces;
using S1EORI_HFT_2022232.Models.Statistics;
using S1EORI_HFT_2022232.Repository;
using System.Collections.Generic;


namespace S1EORI_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGitRepositoryLogic gitRepositoryLogic;
        public StatController(IGitRepositoryLogic gitRepositoryLogic)
        {
            this.gitRepositoryLogic = gitRepositoryLogic;
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
        public int GetCommitCountForRepository(int repositoryId)
        {
            return gitRepositoryLogic.GetCommitCountForRepository(repositoryId);            
        }

    }
}
