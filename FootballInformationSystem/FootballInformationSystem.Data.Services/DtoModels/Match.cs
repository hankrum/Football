using System;
using System.Collections.Generic;
using System.Text;

namespace FootballInformationSystem.Data.Services.DtoModels
{
    public class Match
    {
        public long? Id { get; set; }

        public Competition Competition { get; set; }

        public Team HomeTeam { get; set; }

        public Team GuestTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int GuestTeamGoals { get; set; }

        public DateTime Date { get; set; }
    }
}
