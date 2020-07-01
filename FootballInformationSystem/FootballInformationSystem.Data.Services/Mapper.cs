using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    // TODO: Move it in a separate file
    public interface IMapper
    {
        Dbo.City Map(Dto.City city);

        Dto.City Map(Dbo.City city);

        Dbo.Country Map(Dto.Country country);

        Dto.Country Map(Dbo.Country country);

        Dto.Team Map(Dbo.Team team);

        Dbo.Team Map(Dto.Team team);

        Dto.Game Map(Dbo.Game game);

        Dbo.Game Map(Dto.Game game);

        Dbo.Competition Map(Dto.Competition competition);

        Dto.Competition Map(Dbo.Competition competition);

        IEnumerable<Dto.Team> Map(IList<Dbo.Team> teams);

        IEnumerable<Dto.Game> Map(IList<Dbo.Game> games);
    }

    public class Mapper : IMapper
    {
        public Dbo.City Map(Dto.City city)
        {
            if (city == null)
            {
                return null;
            }

            return new Dbo.City
            {
                CityId = city.Id,
                Name = city.Name
            };
        }

        public Dto.City Map(Dbo.City city)
        {
            if (city == null)
            {
                return null;
            }

            var result = new Dto.City
            {
                Id = city.CityId,
                Name = city.Name
            };

            return result;
        }

        public Dbo.Country Map(Dto.Country country)
        {
            if (country == null)
            {
                return null;
            }

            return new Dbo.Country
            {
                CountryId = country.Id,
                Name = country.Name
            };
        }

        public Dto.Country Map(Dbo.Country country)
        {
            if (country == null)
            {
                return null;
            }

            return new Dto.Country
            {
                Id = country.CountryId,
                Name = country.Name
            };
        }

        public Dto.Team Map(Dbo.Team team)
        {
            if (team == null)
            {
                return null;
            }

            var result = new Dto.Team
            {
                Id = team.TeamId,
                Name = team.Name,
                City = Map(team.City),
                Country = Map(team.Country),
                Competitions = team.Competitions.Select(c => Map(c.Competition)).ToArray()
            };

            return result;
        }

        public Dbo.Team Map(Dto.Team team)
        {
            if (team == null)
            {
                return null;
            }

            var result = new Dbo.Team
            {
                TeamId = team.Id,
                Name = team.Name,
                CityId = team.City.Id,
                CountryId = team.Country.Id,
                Competitions = team.Competitions.Select(competition => Map(competition, team.Id)).ToList()
            };

            return result;
        }

        public Dbo.TeamCompetition Map(Dto.Competition competition, long teamId)
        {
            if (competition == null)
            {
                return null;
            }

            var result = new Dbo.TeamCompetition
            {
                TeamId = teamId,
                Competition = new Dbo.Competition
                {
                    CompetitionId = competition.Id,
                    Name = competition.Name
                }
            };
            return result;
        }

        public Dto.Competition Map(Dbo.Competition competition)
        {
            if (competition == null)
            {
                return null;
            }

            var result = new Dto.Competition
            {
                Id = competition.CompetitionId,
                Name = competition.Name
            };
            return result;
        }

        public Dbo.Competition Map(Dto.Competition competition)
        {
            if (competition == null)
            {
                return null;
            }

            var result = new Dbo.Competition
            {
                CompetitionId = competition.Id,
                Name = competition.Name
            };
            return result;
        }

        public IEnumerable<Dto.Team> Map(IList<Dbo.Team> teams)
        {
            if (teams == null)
            {
                return null;
            }

            var result = teams.Select(team => Map(team));

            return result;
        }

        public Dto.Game Map(Dbo.Game game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new Dto.Game
            {
                Id = game.GameId,
                Competition = Map(game.Competition),
                HomeTeam = Map(game.HomeTeam),
                AwayTeam = Map(game.AwayTeam),
                HomeTeamGoals = game.HomeTeamGoals,
                AwayTeamGoals = game.AwayTeamGoals,
                Date = game.Date,
                MatchFinished = game.GameFinished
            };

            return result;
        }

        public Dbo.Game Map(Dto.Game game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new Dbo.Game
            {
                GameId = game.Id,
                Competition = Map(game.Competition),
                HomeTeam = Map(game.HomeTeam),
                AwayTeam = Map(game.AwayTeam),
                HomeTeamGoals = game.HomeTeamGoals,
                AwayTeamGoals = game.AwayTeamGoals,
                Date = game.Date,
                GameFinished = game.MatchFinished
            };

            return result;
        }

        public IEnumerable<Dto.Game> Map(IList<Dbo.Game> games)
        {
            if (games == null)
            {
                return null;
            }

            var result = games.Select(game => Map(game));

            return result;
        }
    }
}
