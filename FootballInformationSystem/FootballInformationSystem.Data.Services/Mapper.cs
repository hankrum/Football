using FootballInformationSystem.Data.Model;
using System.Collections.Generic;
using System.Linq;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    public interface IMapper
    {
        Dbo.City Map(Dto.City city);

        Dto.City Map(Dbo.City city);

        Dbo.Country Map(Dto.Country country);

        Dto.Country Map(Dbo.Country country);

        Dto.Team Map(Dbo.Team team);

        Dbo.Team Map(Dto.Team team);

        IEnumerable<Dto.Team> Map(IList<Dbo.Team> teams);
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
                Id = team.Id,
                Name = team.Name,
                City = Map(team.City),
                Country = Map(team.Country)
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
                Id = team.Id,
                Name = team.Name,
                CityId = team.City.Id,
 //               City = Map(team.City),
                CountryId = team.Country.Id,
 //               Country = Map(team.Country),
                TeamCompetitions = team.Competitions.Select(competition => Map(competition, team.Id)).ToList()
            };

            return result;
        }

        public Dbo.TeamCompetition Map(Dto.Competition competition, long teamId)
        {
            if (competition == null)
            {
                return null;
            }

            var result = new TeamCompetition
            {
                TeamId = teamId,
                Competition = new Competition
                {
                    CompetitionId = competition.Id,
                    Name = competition.Name
                }
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
    }
}
