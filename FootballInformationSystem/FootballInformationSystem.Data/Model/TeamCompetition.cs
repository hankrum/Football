using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FootballInformationSystem.Data.Model
{
    public class TeamCompetition : SystemInfo
    {
        public long TeamId { get; set; }

        public Team Team { get; set; }

        public long CompetitionId { get; set; }

        public Competition Competition { get; set; }
    }
}
