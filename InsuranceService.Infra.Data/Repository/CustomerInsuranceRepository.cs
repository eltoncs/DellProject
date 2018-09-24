using InsuranceServices.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InsuranceServices.Domain.Entities;
using System.Linq.Expressions;
using InsuranceServices.Infra.Data.Context;
using System.Linq;
using System.Data.Entity;

namespace InsuranceServices.Infra.Data.Repository
{
    public class CustomerInsuranceRepository : Repository<CustomerInsurance>, ICustomerInsuranceRepository
    {
        public CustomerInsuranceRepository(ISContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<CustomerInsurance>> GetAll()
        {
            return await DbSet.Select(x=> x).ToListAsync();
        }
    }
}
