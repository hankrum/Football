using FootballInformationSystem.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballInformationSystem.Data.Services
{
    public interface ICompetitionsService
    {
        Task<bool> Exists(long id);
    }

    public class CompetitionsService
    {
        private readonly IUnitOfWork unitOfWork;

        public CompetitionsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Exists(long id)
        {
            var competition = await this.unitOfWork.Competitions.GetById(id);
            return competition != null && competition.CompetitionId == id;
        }
    }
}
