using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballInformationSystem.Data.Model
{
    public class Game : SystemInfo
    {
        [Key]
        public long GameId { get; set; }

        public long CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        public long? HomeTeamId { get; set; }

        [ForeignKey("HomeTeamId")]
        public virtual Team HomeTeam { get; set; }

        public long? AwayTeamId { get; set; }

        [ForeignKey("AwayTeamId")]

        public virtual Team AwayTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public DateTime Date { get; set; }

        public Boolean MatchFinished { get; set; }
    }
}
