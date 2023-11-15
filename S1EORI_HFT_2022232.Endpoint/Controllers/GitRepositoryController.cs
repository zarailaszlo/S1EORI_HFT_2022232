using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using S1EORI_HFT_2022232.Endpoint.Services;
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
        IHubContext<SignalRHub> hub;

        public GitRepositoryController(IGitRepositoryLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("GitRepositoryCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] GitRepository value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("GitRepositoryUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("GitRepositoryDeleted", id);
        }
    }
}
