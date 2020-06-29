﻿using FootballInformationSystem.Infrastructure;
using System;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    public interface IMapper
    {
        Dbo.City Map(Dto.City city);

        Dbo.Country Map(Dto.Country country);
    }

    public class Mapper : IMapper
    {
        public Dbo.City Map(Dto.City city)
        {
            Validated.NotNull(city, nameof(city));

            return new Dbo.City
            {
                Id = city.Id,
                Name = city.Name
            };
        }

        public Dbo.Country Map(Dto.Country country)
        {
            Validated.NotNull(country, nameof(country));

            return new Dbo.Country
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}
