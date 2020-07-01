using FootballInformationSystem.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FootballInformationSystem.Data
{
    public class MsSqlDbContext : DbContext, IMsSqlDbContext
    {
        public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Team> Teams { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.UpdateSoftDelete();
            this.UpdateCreateAndModify();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<City>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            builder.Entity<Competition>().HasQueryFilter(c => EF.Property<bool>(c, "IsDeleted") == false);

            builder.Entity<Country>().HasQueryFilter(c => EF.Property<bool>(c, "IsDeleted") == false);

            builder.Entity<Game>().HasQueryFilter(c => EF.Property<bool>(c, "IsDeleted") == false);

            builder.Entity<Team>().HasQueryFilter(c => EF.Property<bool>(c, "IsDeleted") == false);

            builder.Entity<TeamCompetition>()
                .HasKey(t => new { t.TeamId, t.CompetitionId });

            builder.Entity<Team>()
                .HasOne(t => t.City)
                .WithMany(c => c.Teams)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Team>()
                .HasOne(t => t.Country)
                .WithMany(c => c.Teams)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<TeamCompetition>()
                .HasOne(tc => tc.Team)
                .WithMany(t => t.Competitions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<TeamCompetition>()
                .HasOne(tc => tc.Competition)
                .WithMany(t => t.TeamCompetitions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Game>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void UpdateSoftDelete()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                var entity = (IDeletable)entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.IsDeleted = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entity.IsDeleted = true;
                        entity.DeletedOn = DateTime.Now;
                        break;
                }
            }
        }

        private void UpdateCreateAndModify()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = (IAuditable)entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedOn = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entity.ModifiedOn = DateTime.Now;
                        break;
                }
            }
        }
    }
}


