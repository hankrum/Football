using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FootballInformationSystem.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    public interface IRankingsService
    {
        Task<IEnumerable<Dto.CompetitionRanking>> All();
    }

    public class RankingsService : IRankingsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGamesService gamesService;
        private readonly IMapper mapper;

        public RankingsService(IUnitOfWork unitOfWork, IGamesService gamesService, IMapper mapper)
        {
            this.gamesService = gamesService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Dto.CompetitionRanking>> All()
        {
            var competitions = await this.unitOfWork.Competitions.All()
                .Include(c => c.Teams)
                .ThenInclude(tc => tc.Team)
                .ToListAsync();

            var result = competitions.Select(competition => new Dto.CompetitionRanking
            {
                Competition = mapper.Map(competition),
                TeamRankings = competition.Teams.Select(team => new TeamRanking
                {
                    Team = new Dto.Team { Id = team.TeamId, Name = team.Team.Name },
                    Points = this.gamesService.getPointsInCompetition(team.Team.Name, competition.Name).Result
                }).OrderByDescending(tr => tr.Points).ToArray()
            });

            return result;
        }
    }
}
