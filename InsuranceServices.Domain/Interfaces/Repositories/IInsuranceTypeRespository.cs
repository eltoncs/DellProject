using InsuranceServices.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Repositories
{
    public interface IInsuranceTypeRepository : IRepository<InsuranceType>
    {
        Task<InsuranceType> GetByName(string name);
        Task<IEnumerable<InsuranceType>> GetAll();
        Task<IEnumerable<string>> GetComboList();
    }
}
