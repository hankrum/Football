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
