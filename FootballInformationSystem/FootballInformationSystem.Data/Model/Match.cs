using System;
using System.ComponentModel.DataAnnotations;

namespace FootballInformationSystem.Data.Model
{
    public class Match : SystemInfo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public virtual Competition Competition { get; set; }

        [Required]
        public virtual Team HomeTeam { get; set; }

        [Required]
        public virtual Team GuestTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int GuestTeamGoals { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
