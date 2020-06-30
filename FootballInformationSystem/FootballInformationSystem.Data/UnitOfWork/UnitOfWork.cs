using FootballInformationSystem.Data.Model;
using FootballInformationSystem.Data.Repository;
using FootballInformationSystem.Infrastructure;
using System.Threading.Tasks;

namespace FootballInformationSystem.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MsSqlDbContext context;
        private readonly IEfRepository<City> cities;
        private readonly IEfRepository<Competition> competitions;
        private readonly IEfRepository<Country> countries;
        private readonly IEfRepository<Game> games;
        private readonly IEfRepository<Team> teams;

        public UnitOfWork(
            MsSqlDbContext context,
            IEfRepository<City> cities,
            IEfRepository<Competition> competitions,
            IEfRepository<Country> countries,
            IEfRepository<Game> games,
            IEfRepository<Team> teams
            )
        {
            Validated.NotNull(context, nameof(context));
            Validated.NotNull(cities, nameof(cities));
            Validated.NotNull(competitions, nameof(competitions));
            Validated.NotNull(countries, nameof(countries));
            Validated.NotNull(games, nameof(games));
            Validated.NotNull(teams, nameof(teams));
            this.context = context;
            this.cities = cities;
            this.competitions = competitions;
            this.countries = countries;
            this.games = games;
            this.teams = teams;
        }

        public IEfRepository<City> Cities
        {
            get
            {
                return this.cities;
            }
        }

        public IEfRepository<Competition> Competitions
        {
            get
            {
                return this.competitions;
            }
        }

        public IEfRepository<Country> Countries
        {
            get
            {
                return this.countries;
            }
        }

        public IEfRepository<Game> Games
        {
            get
            {
                return this.games;
            }
        }

        public IEfRepository<Team> Teams
        {
            get
            {
                return this.teams;
            }
        }

        public async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }
    }
}
