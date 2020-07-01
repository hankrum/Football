using FootballInformationSystem.Data.Services;
using FootballInformationSystem.Data.Services.DtoModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballInformationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingsController : Controller
    {
        private readonly IRankingsService rankingsService;

        public RankingsController(IRankingsService rankingsService)
        {
            this.rankingsService = rankingsService;
        }

        // GET: api/Ranking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetitionRanking>>> Get()
        {
            return Ok(await this.rankingsService.All());
        }
    }
}
