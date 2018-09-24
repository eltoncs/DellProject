using InsuranceServices.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Repositories
{
    public interface IPartnerRepository: IRepository<Partner>
    {
        Task<IEnumerable<Partner>> GetAll();
        Task<Partner> GetByName(string name);
        Task<IEnumerable<Partner>> GetAllWithSimulations();
    }
}
