using InsuranceServices.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByName(string name);
    }
}
