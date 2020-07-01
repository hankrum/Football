using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballInformationSystem.Data.Model
{
    public class Competition : SystemInfo
    {
        [Key]
        public long CompetitionId { get; set; }


        public string Name { get; set; }

        public virtual IList<TeamCompetition> Teams { get; set; }
    }
}
