using InsuranceServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Services
{
    public interface IStatisticService: IDisposable
    {
        Task<Statistic> Save(Statistic statistic, Guid id);
        Task<IEnumerable<Statistic>> GetAll();
        Task<IEnumerable<Statistic>> GetByPartner(Guid partnerId);
        Task Remove(Guid Id);
    }
}
