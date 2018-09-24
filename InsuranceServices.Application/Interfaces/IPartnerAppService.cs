using InsuranceServices.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Interfaces
{
    public interface IPartnerAppService : IAppService<PartnerViewModel>
    {
        Task<PartnerViewModel> GetByName(string name);
        Task<IEnumerable<PartnerViewModel>> GetAll();

        Task<IEnumerable<PartnerViewModel>> GetAllWithSimulations();
    }
}
