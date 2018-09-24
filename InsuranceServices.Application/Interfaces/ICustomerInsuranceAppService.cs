using InsuranceServices.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Interfaces
{
    public interface ICustomerInsuranceAppService : IAppService<CustomerInsuranceViewModel>
    {
        Task<IEnumerable<CustomerInsuranceViewModel>> GetAll();
    }
}
