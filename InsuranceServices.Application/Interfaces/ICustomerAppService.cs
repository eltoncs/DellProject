using InsuranceServices.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Interfaces
{
    public interface ICustomerAppService: IAppService<CustomerViewModel>
    {
        Task<CustomerViewModel> GetByName(string name);
        Task<IEnumerable<CustomerViewModel>> GetAll();        
    }
}
