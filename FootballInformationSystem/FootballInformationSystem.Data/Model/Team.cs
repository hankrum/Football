using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballInformationSystem.Data.Model
{
    public class Team : SystemInfo
    {
        [Key]
        public long TeamId { get; set; }

        public string Name { get; set; }

        public long CityId { get; set; }

        public virtual City City { get; set; }

        public long CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual IList<TeamCompetition> Competitions { get; set; }

        public virtual IList<Game> HomeGames { get; set; }

        public virtual IList<Game> AwayGames { get; set; }
    }
}
