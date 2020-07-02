using FootballInformationSystem.Data.UnitOfWork;
using FootballInformationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dbo = FootballInformationSystem.Data.Model;
using Dto = FootballInformationSystem.Data.Services.DtoModels;

namespace FootballInformationSystem.Data.Services
{
    public interface ICompetitionsService
    {
        Task<bool> Exists(long id);

        Task<IEnumerable<Dto.Competition>> All();

        Task<Dbo.Competition> GetByName(string name);

        Task<Dbo.Competition> Create(Dto.Competition competition);
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

        public async Task<Dbo.Competition> GetByName(string name)
        {
            var result = await this.unitOfWork.Competitions.All().Where(c => c.Name == name).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<bool> Exists(long id)
        {
            var competition = await this.unitOfWork.Competitions.GetById(id);
            return competition != null && competition.CompetitionId == id;
        }

        public async Task<Dbo.Competition> Create(Dto.Competition competition)
        {
            Validated.NotNull(competition, nameof(competition));

            Dbo.Competition addedCompetition = this.unitOfWork.Competitions.Add(mapper.Map(competition));

            await this.unitOfWork.SaveChanges();

            return addedCompetition;
        }
    }
}
