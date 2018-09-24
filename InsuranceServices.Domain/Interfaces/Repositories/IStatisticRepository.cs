using InsuranceServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Repositories
{
    public interface IStatisticRepository: IRepository<Statistic>
    {
        Task<IEnumerable<Statistic>> GetAll();
        Task<IEnumerable<Statistic>> GetByPartner(Guid partnerId);
    }
}
