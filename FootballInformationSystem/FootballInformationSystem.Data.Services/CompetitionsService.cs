using FootballInformationSystem.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    public interface ICompetitionsService
    {
        Task<bool> Exists(long id);

        Task<IEnumerable<Dto.Competition>> All();
    }

    public class CompetitionsService : ICompetitionsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CompetitionsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Dto.Competition>> All()
        {
            var result = await this.unitOfWork.Competitions.All().ToListAsync();

            var mappedResult = result.Select(c => mapper.Map(c));

            return mappedResult;
        }

        public async Task<bool> Exists(long id)
        {
            var competition = await this.unitOfWork.Competitions.GetById(id);
            return competition != null && competition.CompetitionId == id;
        }
    }
}
