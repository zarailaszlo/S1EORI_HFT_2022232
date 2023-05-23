using Microsoft.AspNetCore.Mvc;
using S1EORI_HFT_2022232.Logic.Interfaces;
using S1EORI_HFT_2022232.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace S1EORI_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GitRepositoryController : ControllerBase
    {
        IGitRepositoryLogic logic;

        public GitRepositoryController(IGitRepositoryLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<GitRepository> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public GitRepository Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] GitRepository value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] GitRepository value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
