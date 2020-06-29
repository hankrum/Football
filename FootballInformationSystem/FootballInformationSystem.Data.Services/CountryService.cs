using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;
using FootballInformationSystem.Data.UnitOfWork;
using FootballInformationSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FootballInformationSystem.Data.Services
{
    public class CountryService
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
