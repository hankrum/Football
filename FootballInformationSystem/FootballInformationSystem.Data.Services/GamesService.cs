﻿using FootballInformationSystem.Data.UnitOfWork;
using FootballInformationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    // TODO: Move it in a separate file
    public interface IGamesService
    {
        Task<IEnumerable<Dto.Game>> All();

        Task<Dto.Game> Create(Dto.Game game);

        Task<Dto.Game> Update(Dto.Game game);

        Task<Dto.Game> Delete(long id);

        Task<bool> Exists(long id);

        Task<bool> GameFinished(long id);
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
                        GameFinished = game.GameFinished
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
                        GameFinished = game.GameFinished
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
                        GameFinished = game.GameFinished
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

        public async Task<Dto.Game> Update(Dto.Game game)
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

            Dbo.Game updatedGame = this.unitOfWork.Games.Update(mapper.Map(game));

            await this.unitOfWork.SaveChanges();

            return mapper.Map(updatedGame);
        }

        public async Task<Dto.Game> Delete(long id)
        {
            var model = await this.unitOfWork.Games.GetById(id);
            Dbo.Game deletedGame = this.unitOfWork.Games.Delete(model);

            await this.unitOfWork.SaveChanges();

            return mapper.Map(deletedGame);
        }

        public async Task<bool> Exists(long id)
        {
            var game = await this.unitOfWork.Games.GetById(id);
            return game != null && game.GameId == id;
        }

        public async Task<bool> GameFinished(long id)
        {
            var game = await this.unitOfWork.Games.GetById(id);
            return game.GameFinished;
        }
    }
}
