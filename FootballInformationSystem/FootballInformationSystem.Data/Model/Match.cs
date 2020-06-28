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

        [Required]
        public int HomeTeamGoals { get; set; }

        [Required]
        public int GuestTeamGoals { get; set; }
    }
}
