using InsuranceServices.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Interfaces
{
    public interface IStatisticAppService : IAppService<StatisticViewModel>
    {
        Task<IEnumerable<StatisticViewModel>> GetAll();
    }
}

