using InsuranceServices.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using InsuranceServices.Domain.Entities;
using InsuranceServices.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Services
{
    public class StatisticService: IStatisticService
    {
        private readonly IStatisticRepository _statisticRepository;

        public StatisticService(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        public async Task<IEnumerable<Statistic>> GetAll()
        {
            return await _statisticRepository.GetAll();
        }

        public async Task<IEnumerable<Statistic>> GetByPartner(Guid partnerId)
        {
            return await _statisticRepository.GetByPartner(partnerId);
        }

        public async Task Remove(Guid id)
        {
            await _statisticRepository.Remove(id);
        }

        public async Task<Statistic> Save(Statistic statistic, Guid id)
        {
            return await _statisticRepository.Save(statistic, id);
        }

        public void Dispose()
        {
            _statisticRepository.Dispose();
            GC.SuppressFinalize(this);
        }      
    }
}
