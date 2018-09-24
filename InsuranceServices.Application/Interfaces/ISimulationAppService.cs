using InsuranceServices.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Interfaces
{
    public interface ISimulationAppService
    {
        Task<Decimal> Simulate(SimulationViewModel simulation);
        Task<IEnumerable<GeneralStatisticViewModel>> GetAllSimulations();
        Task<IEnumerable<SummaryStatisticViewModel>> GetSummaryStatistics(Guid? partnerId);
    }
}
