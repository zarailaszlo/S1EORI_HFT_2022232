using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using S1EORI_HFT_2022232.Endpoint.Services;
using S1EORI_HFT_2022232.Logic.Interfaces;
using S1EORI_HFT_2022232.Models;
using System.Collections.Generic;


namespace S1EORI_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommitController : ControllerBase
    {
        ICommitLogic logic;
        IHubContext<SignalRHub> hub;

        public CommitController(ICommitLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Commit> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Commit Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Commit value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CommitCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Commit value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CommitUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var commitToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CommitDeleted", commitToDelete);
        }
    }
}
