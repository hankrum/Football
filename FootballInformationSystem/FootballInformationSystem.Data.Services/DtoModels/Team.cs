using System;
using System.Collections.Generic;
using System.Text;

namespace FootballInformationSystem.Data.Services.DtoModels
{
    public class Team
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public City City { get; set; }

        public Country Country { get; set; }
    }
}
