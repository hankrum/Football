using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballInformationSystem.Data.Model
{
    public class City : SystemInfo
    {
        [Key]
        public long CityId { get; set; }

        public string Name { get; set; }

        public virtual HashSet<Team> Teams { get; set; }
    }
}
