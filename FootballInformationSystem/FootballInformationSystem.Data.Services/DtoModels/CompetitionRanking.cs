namespace FootballInformationSystem.Data.Services.DtoModels
{
    public class CompetitionRanking
    {
        public Competition Competition { get; set; }

        public TeamRanking[] TeamRankings { get; set; }
    }
}
