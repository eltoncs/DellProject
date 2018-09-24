using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Entities;
using InsuranceServices.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InsuranceServices.Infra.Data.Repository
{
    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(ISContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<Partner>> GetAll()
        {
            return await DbSet.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<Partner>> GetAllWithSimulations()
        {
            return await DbSet.Where(x=> x.Simulations.Any()).ToListAsync();
        }

        public async Task<Partner> GetByName(string name)
        {
            var partner = await DbSet.FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());
            return partner;
        }
    }
}
