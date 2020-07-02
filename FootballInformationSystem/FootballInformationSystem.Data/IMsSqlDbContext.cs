using FootballInformationSystem.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace FootballInformationSystem.Data
{
    public interface IMsSqlDbContext
    {
        DbSet<City> Cities { get; set; }

        DbSet<Competition> Competitions { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Game> Games { get; set; }

        DbSet<Team> Teams { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
