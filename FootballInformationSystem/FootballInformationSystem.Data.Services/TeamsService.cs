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
    public interface ITeamsService
    {
        Task<IEnumerable<Dto.Team>> All();

        Task<Dto.Team> Create(Dto.Team team);

        Task<Dbo.Team> GetByName(string name);
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

        public async Task<IEnumerable<Dto.Team>> All()
        {
            var teams = await this.unitOfWork.Teams.All().ToListAsync();

            var result = mapper.Map(teams);

            return result;
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

            team.City.Id = city.Id;

            Dbo.Country country = await this.countryService.GetByName(team.Country.Name);

            if (country == null)
            {
                country = await this.countryService.Create(team.Country);
            }

            team.Country.Id = country.Id;

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

            team.City.Id = city.Id;

            Dbo.Country country = await this.countryService.GetByName(team.Country.Name);

            if (country == null)
            {
                country = await this.countryService.Create(team.Country);
            }

            team.Country.Id = country.Id;

            Dbo.Team updatedTeam = this.unitOfWork.Teams.Update(mapper.Map(team));

            await this.unitOfWork.SaveChanges();

            return mapper.Map(updatedTeam);
        }
    }
}
