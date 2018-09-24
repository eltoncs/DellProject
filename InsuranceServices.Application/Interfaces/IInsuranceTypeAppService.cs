using InsuranceServices.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Interfaces
{
    public interface IInsuranceTypeAppService : IAppService<InsuranceTypeViewModel>
    {
        Task<InsuranceTypeViewModel> GetByName(string name);
        Task<IEnumerable<InsuranceTypeViewModel>> GetAll();
        Task<IEnumerable<string>> GetComboList();
        Task<IEnumerable<InsuranceTypeCheckListViewModel>> GetCheckList();

    }
}
