using FootballInformationSystem.Data.UnitOfWork;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using FootballInformationSystem.Data.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FootballInformationSystem.Infrastructure;

namespace FootballInformationSystem.Data.Services
{
    // TODO: Move it in a separate file
    public interface IGamesService
    {
        Task<IEnumerable<Dto.Game>> All();
    }

    public class GamesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICompetitionsService competitionsService;
        private readonly ITeamsService teamsService;

        public GamesService(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            ICompetitionsService competitionsService,
            ITeamsService teamsService
        )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.competitionsService = competitionsService;
            this.teamsService = teamsService;
        }

        public async Task<IEnumerable<Dto.Game>> All()
        {
            var games = this.unitOfWork.Games.All();
            var teams = this.unitOfWork.Teams.All();
            var competitions = this.unitOfWork.Competitions.All();

            var joined = games.Join(
                    competitions,
                    game => game.CompetitionId,
                    competition => competition.CompetitionId,
                    (game, competition) => new Dbo.Game
                    {
                        GameId = game.GameId,
                        Competition = competition,
                        HomeTeamId = game.HomeTeamId,
                        AwayTeamId = game.AwayTeamId,
                        HomeTeamGoals = game.HomeTeamGoals,
                        AwayTeamGoals = game.AwayTeamGoals,
                        Date = game.Date,
                        MatchFinished = game.MatchFinished
                    }
                )
                .Join(
                    teams,
                    game => game.HomeTeamId,
                    team => team.Id,
                    (game, team) => new Dbo.Game
                    {
                        GameId = game.GameId,
                        Competition = game.Competition,
                        HomeTeam = team,
                        AwayTeamId = game.AwayTeamId,
                        HomeTeamGoals = game.HomeTeamGoals,
                        AwayTeamGoals = game.AwayTeamGoals,
                        Date = game.Date,
                        MatchFinished = game.MatchFinished
                    }
                )
                 .Join(
                    teams,
                    game => game.AwayTeamId,
                    team => team.Id,
                    (game, team) => new Dbo.Game
                    {
                        GameId = game.GameId,
                        Competition = game.Competition,
                        HomeTeam = game.HomeTeam,
                        AwayTeam = team,
                        HomeTeamGoals = game.HomeTeamGoals,
                        AwayTeamGoals = game.AwayTeamGoals,
                        Date = game.Date,
                        MatchFinished = game.MatchFinished
                    }
               );


            var result = await joined.ToListAsync();

            var mapped = mapper.Map(result);

            return mapped;
        }

        public async Task<Dto.Game> Create(Dto.Game game)
        {
            Validated.NotNull(game, nameof(game));

            bool competitionExists = await this.competitionsService.Exists(game.Competition.Id);
            if (!competitionExists)
            {
                throw new ArgumentException("No competition");
            }

            bool homeTeamExists = await this.teamsService.Exists(game.HomeTeam.Id);
            if (!homeTeamExists)
            {
                throw new ArgumentException("No home team");
            }

            bool awayTeamExists = await this.teamsService.Exists(game.AwayTeam.Id);
            if (!awayTeamExists)
            {
                throw new ArgumentException("No away team");
            }

            Dbo.Game addedGame = this.unitOfWork.Games.Add(mapper.Map(game));


            await this.unitOfWork.SaveChanges();

            return mapper.Map(addedGame);
        }

        public async Task<Dto.Game> Delete(long id)
        {
            var model = await this.unitOfWork.Games.GetById(id);
            Dbo.Game deletedGame = this.unitOfWork.Games.Delete(model);

            await this.unitOfWork.SaveChanges();

            return mapper.Map(deletedGame);
        }

    }
}
