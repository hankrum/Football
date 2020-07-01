﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FootballInformationSystem.Data.Services.DtoModels
{
    public class Competition
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Team[] Teams { get; set; }
    }
}
