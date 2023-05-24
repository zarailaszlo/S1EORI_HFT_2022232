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
        //ICommitLogic commitLogic;
        //IUserLogic userLogic;
        public StatController(IGitRepositoryLogic gitRepositoryLogic)
        {
            this.gitRepositoryLogic = gitRepositoryLogic;
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
    }
}
