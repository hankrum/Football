using FootballInformationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballInformationSystem.Data.Repository
{
    public class EFRepository<T> : IEfRepository<T> where T : class
    {
        private readonly MsSqlDbContext context;


        public EFRepository(MsSqlDbContext context)
        {
            Validated.NotNull(context, nameof(context));
            this.context = context;
        }

        public IQueryable<T> All()
        {
            var queryResult = this.context.Set<T>().AsQueryable();
            return queryResult;
        }

        public T Add(T entity)
        {
            Validated.NotNull(entity, nameof(entity));
            EntityEntry entry = this.context.Entry(entity);

            return this.context.Set<T>().Add(entity).Entity;
        }

        public T Delete(T entity)
        {
            Validated.NotNull(entity, nameof(entity));
            var entry = this.context.Entry(entity);

            return this.context.Set<T>().Remove(entity).Entity;
        }

        public T Update(T entity)
        {
            Validated.NotNull(entity, nameof(entity));
            EntityEntry entry = this.context.Entry(entity);

            return this.context.Set<T>().Update(entity).Entity;
        }

        public async Task<T> GetById(long id)
        {
            return await this.context.Set<T>().FindAsync(id);
        }
    }
}
