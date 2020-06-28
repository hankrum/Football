using System.Linq;
using System.Threading.Tasks;

namespace FootballInformationSystem.Data.Repository
{
    public interface IEfRepository<T> where T : class
    {
        IQueryable<T> All();

        T Add(T entity);

        T Delete(T entity);

        T Update(T entity);

        Task<T> GetById(long id);
    }
}
