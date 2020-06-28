using FootballInformationSystem.Data.Model;
using FootballInformationSystem.Data.Repository;
using System.Threading.Tasks;

namespace FootballInformationSystem.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEfRepository<City> Cities { get; }

        IEfRepository<Competition> Competitions { get; }

        IEfRepository<Country> Countries { get; }

        IEfRepository<Match> Matches { get; }

        IEfRepository<Team> Teams { get; }

        Task<int> SaveChanges();
    }
}
