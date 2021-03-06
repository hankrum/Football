﻿using FootballInformationSystem.Data.UnitOfWork;
using FootballInformationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    public interface ICityService
    {
        Task<Dbo.City> GetByName(string name);

        Task<Dbo.City> GetById(long id);

        Task<Dbo.City> Create(Dto.City city);
    }

    public class CityService : ICityService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Validated.NotNull(unitOfWork, nameof(unitOfWork));
            Validated.NotNull(mapper, nameof(mapper));
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Dbo.City> GetByName(string name)
        {
            var result = await this.unitOfWork.Cities.All().Where(c => c.Name == name).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<Dbo.City> GetById(long id)
        {
            return await this.unitOfWork.Cities.GetById(id);
        }

        public async Task<Dbo.City> Create(Dto.City city)
        {
            Validated.NotNull(city, nameof(city));

            Dbo.City addedCity = this.unitOfWork.Cities.Add(mapper.Map(city));

            await this.unitOfWork.SaveChanges();

            return addedCity;
        }
    }
}
