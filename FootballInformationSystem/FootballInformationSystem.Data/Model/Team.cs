using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballInformationSystem.Data.Model
{
    public class Team : SystemInfo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual City City { get; set; }

        [Required]
        public virtual Country Country { get; set; }

        public virtual HashSet<Competition> Competitions { get; set; }

        public virtual HashSet<Match> Matches { get; set; }
    }
}
