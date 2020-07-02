using System;

namespace FootballInformationSystem.Data.Services.DtoModels
{
    public class Game
    {
        public long Id { get; set; }

        public Competition Competition { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public DateTime Date { get; set; }

        public bool MatchFinished { get; set; }
    }
}
