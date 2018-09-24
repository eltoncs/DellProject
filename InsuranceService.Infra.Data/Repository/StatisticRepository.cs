using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Entities;
using InsuranceServices.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace InsuranceServices.Infra.Data.Repository
{
    public class StatisticRepository: Repository<Statistic>, IStatisticRepository
    {
        public StatisticRepository(ISContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<Statistic>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<Statistic>> GetByPartner(Guid partnerId)
        {
            return await DbSet.Where(x=> x.PartnerId == partnerId).OrderBy(x=> x.CreationDate).ToListAsync();
        }       
    }
}
