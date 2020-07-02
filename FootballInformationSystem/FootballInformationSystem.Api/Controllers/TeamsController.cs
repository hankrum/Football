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
        public async Task<ActionResult<IEnumerable<Team>>> Get(string cityName = null, string countryName = null)
        {
            var result = await this.teamsService.All(cityName, countryName);

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
            string empty = string.Empty;

            if (team == null || team.Name == empty || team.City.Name == empty || team.Country.Name == empty || team.Competitions.Any(c => c.Name == empty))
            {
                return Problem("Not valid");
            }

            var teamResult = await this.teamsService.GetByName(team.Name);

            Team result;
            if (teamResult == null)
            {
                result = await this.teamsService.Create(team);
            }
            else
            {
                team.Id = teamResult.TeamId;

                result = await this.teamsService.Update(team);
            }

            return CreatedAtAction(
                nameof(this.Post),
                new { id = team.Id },
                result
            );
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Team>> Put(long id, [FromBody] Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            if (!await this.teamsService.Exists(id))
            {
                return NotFound();
            }

            var result = await this.teamsService.Update(team);

            return result;
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> Delete(int id)
        {
            if (!await this.teamsService.Exists(id))
            {
                return NotFound();
            }

            var result = await this.teamsService.Delete(id);

            return result;
        }
    }
}
