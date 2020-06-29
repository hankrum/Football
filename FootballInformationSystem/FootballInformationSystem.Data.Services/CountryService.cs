using FootballInformationSystem.Data.UnitOfWork;
using FootballInformationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    public interface ICountryService
    {
        Task<Dbo.Country> GetByName(string name);

        Task<Dbo.Country> GetById(long id);

        Task<Dbo.Country> Create(Dto.Country country);
    }

    public class CountryService: ICountryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Validated.NotNull(unitOfWork, nameof(unitOfWork));
            Validated.NotNull(mapper, nameof(mapper));
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Dbo.Country> GetByName(string name)
        {
            var result = await this.unitOfWork.Countries.All().Where(c => c.Name == name).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<Dbo.Country> GetById(long id)
        {
            return await this.unitOfWork.Countries.GetById(id);
        }

        public async Task<Dbo.Country> Create(Dto.Country country)
        {
            Validated.NotNull(country, nameof(country));

            Dbo.Country addedCountry = this.unitOfWork.Countries.Add(mapper.Map(country));

            await this.unitOfWork.SaveChanges();

            return addedCountry;
        }
    }
}
