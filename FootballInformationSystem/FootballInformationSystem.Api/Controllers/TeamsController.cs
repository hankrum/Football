using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballInformationSystem.Data.Services;
using FootballInformationSystem.Data.Services.DtoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballInformationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : Controller
    {
        private ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> Get()
        {
            var result = await this.teamsService.All();

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        // POST: api/Team
        [HttpPost]
        public async Task<ActionResult<Team>> Post([FromBody] Team team)
        {
            var websiteResult = await this.teamsService.GetByName(team.Name);

            Team result;
            if (websiteResult == null)
            {
                result = await this.teamsService.Create(team);
            }
            else
            {
                team.Id = websiteResult.Id;

                result = await this.teamsService.Update(webSite);
            }

            return CreatedAtAction(
                nameof(this.Post),
                new { id = team.Id },
                result
            );
        }
    }
}
