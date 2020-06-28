using FootballInformationSystem.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballInformationSystem.Data
{
    public interface IMsSqlDbContext
    {
        DbSet<City> Cities { get; set; }

        DbSet<Competition> Competitions { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Match> Matches { get; set; }

        DbSet<Team> Teams { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
