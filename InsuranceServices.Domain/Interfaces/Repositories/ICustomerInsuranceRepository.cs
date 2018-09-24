using InsuranceServices.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Repositories 
{
    public interface ICustomerInsuranceRepository : IRepository<CustomerInsurance>
    {
        Task<IEnumerable<CustomerInsurance>> GetAll();
    }
}
