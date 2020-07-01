using FootballInformationSystem.Data.Services;
using FootballInformationSystem.Data.Services.DtoModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballInformationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private IGamesService gamesService;

        public GamesController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            var result = await this.gamesService.All();

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> Post([FromBody] Game game)
        {
            if (game == null)
            {
                return NoContent();
            }

            if (this.gamesService.Exists(game))
            {
                return Problem("Game exists");
            }

            Game result = await this.gamesService.Create(game);

            return CreatedAtAction(
                nameof(this.Post),
                new { id = game.Id },
                result
            );
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> Put(long id, [FromBody] Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            if (!await this.gamesService.Exists(id))
            {
                return NotFound();
            }

            var result = await this.gamesService.Update(game);

            return result;
        }

        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> Delete(int id)
        {
            if (!await this.gamesService.Exists(id))
            {
                return NotFound();
            }

            if(!await this.gamesService.GameFinished(id))
            {
                return Problem("Game not finished");
            }

            var result = await this.gamesService.Delete(id);

            return result;
        }
    }
}
