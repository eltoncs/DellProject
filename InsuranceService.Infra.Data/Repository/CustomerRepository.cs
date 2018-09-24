using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Entities;
using InsuranceServices.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InsuranceServices.Infra.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ISContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await  DbSet.OrderBy(x=> x.Name).ToListAsync(); 
        }

        public async Task<Customer> GetByName(string name)
        {
            var customer = await DbSet.FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());
            return customer;
        }        
    }
}
