using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballInformationSystem.Data.Model
{
    public class Team : SystemInfo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public long CityId { get; set; }

        [Required]
        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }

        public long CountryId { get; set; }

        [Required]
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }

        public virtual HashSet<Competition> Competitions { get; set; }

        public virtual HashSet<Match> Matches { get; set; }
    }
}
