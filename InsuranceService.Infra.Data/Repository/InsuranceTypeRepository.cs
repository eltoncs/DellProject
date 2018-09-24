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
    public class InsuranceTypeRepository: Repository<InsuranceType>, IInsuranceTypeRepository
    {
        public InsuranceTypeRepository(ISContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<InsuranceType>> GetAll()
        {
            return await DbSet.OrderBy(x=> x.Name).ToListAsync();
        }

        public async Task<InsuranceType> GetByName(string name)
        {
            var insuranceType = await DbSet.FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());
            return insuranceType;
        }

        public async Task<IEnumerable<string>> GetComboList()
        {
            var ret = new List<string>();
            var insuranceTypes = await DbSet.OrderBy(x => x.Name).ToListAsync();

            foreach(var item in insuranceTypes)
            {
                ret.Add(item.Name);
            }

            return ret;
        }
    }
}
