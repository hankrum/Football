using FootballInformationSystem.Data.UnitOfWork;
using FootballInformationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
     // TODO: Move it in a separate file
   public interface ITeamsService
    {
        Task<IEnumerable<Dto.Team>> All(string cityName = null, string countryName = null);

        Task<Dto.Team> Create(Dto.Team team);

        Task<Dbo.Team> GetByName(string name);

        Task<Dto.Team> Update(Dto.Team team);

        Task<Dto.Team> Delete(long id);
        
        Task<bool> Exists(long id);
    }

    public class TeamsService : ITeamsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly IMapper mapper;

        public TeamsService(
            IUnitOfWork unitOfWork,
            ICityService cityService,
            ICountryService countryService,
            IMapper mapper
            )
        {
            this.unitOfWork = unitOfWork;
            this.cityService = cityService;
            this.countryService = countryService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Dto.Team>> All(string cityName = null, string countryName = null)
        {
            var city = await this.cityService.GetByName(cityName);
            var country = await this.countryService.GetByName(countryName);

            var teams = this.unitOfWork.Teams.All()
                .Where(t => (cityName == null || t.CityId == city.CityId)
                    && (countryName == null || t.CountryId == country.CountryId));

            var cities = this.unitOfWork.Cities.All();
            var countries = this.unitOfWork.Countries.All();
            var competitions = this.unitOfWork.Competitions.All();

            var joined = teams.Join(
                    cities,
                    team => team.CityId,
                    city => city.CityId,
                    (team, city) => new Dbo.Team
                    {
                        Id = team.Id,
                        Name = team.Name,
                        City = city,
                        CountryId = team.CountryId,
                    }
                )
                .Join(
                    countries,
                    team => team.CountryId,
                    country => country.CountryId,
                    (team, country) => new Dbo.Team
                    {
                        Id = team.Id,
                        Name = team.Name,
                        City = team.City,
                        Country = country,
                    }
                );

            var result = await joined.ToListAsync();

            var mapped = mapper.Map(result);

            return mapped;
        }

        public async Task<Dbo.Team> GetByName(string name)
        {
            var result = await this.unitOfWork.Teams.All().Where(c => c.Name == name).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<Dto.Team> Create(Dto.Team team)
        {
            Validated.NotNull(team, nameof(team));

            Dbo.City city = await this.cityService.GetByName(team.City.Name);

            if (city == null)
            {
                city = await this.cityService.Create(team.City);
            }

            team.City.Id = city.CityId;

            Dbo.Country country = await this.countryService.GetByName(team.Country.Name);

            if (country == null)
            {
                country = await this.countryService.Create(team.Country);
            }

            team.Country.Id = country.CountryId;

            Dbo.Team addedTeam = this.unitOfWork.Teams.Add(mapper.Map(team));

            await this.unitOfWork.SaveChanges();

            return mapper.Map(addedTeam);
        }

        public async Task<Dto.Team> Update(Dto.Team team)
        {
            Validated.NotNull(team, nameof(team));

            Dbo.City city = await this.cityService.GetByName(team.City.Name);

            if (city == null)
            {
                city = await this.cityService.Create(team.City);
            }

            team.City.Id = city.CityId;

            Dbo.Country country = await this.countryService.GetByName(team.Country.Name);

            if (country == null)
            {
                country = await this.countryService.Create(team.Country);
            }

            team.Country.Id = country.CountryId;

            Dbo.Team updatedTeam = this.unitOfWork.Teams.Update(mapper.Map(team));

            await this.unitOfWork.SaveChanges();

            return mapper.Map(updatedTeam);
        }

        public async Task<Dto.Team> Delete(long id)
        {
            var model = await this.unitOfWork.Teams.GetById(id);
            Dbo.Team deletedTeam = this.unitOfWork.Teams.Delete(model);

            await this.unitOfWork.SaveChanges();

            return mapper.Map(deletedTeam);
        }

        public async Task<bool> Exists(long id)
        {
            var team = await this.unitOfWork.Teams.GetById(id);
            return team != null && team.Id == id;
        }
    }
}
