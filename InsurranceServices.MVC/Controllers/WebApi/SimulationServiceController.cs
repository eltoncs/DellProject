using InsuranceServices.Application.Interfaces;
using InsuranceServices.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace InsurranceServices.MVC.Controllers.WebApi
{
    [RoutePrefix("Api/SimulationService")]
    public class SimulationServiceController : ApiController
    {
        private ISimulationAppService _simulationAppService;
        private IPartnerAppService _partnerAppService;

        public SimulationServiceController(ISimulationAppService simulationAppService, 
                                           IPartnerAppService partnerAppService)
        {
            _simulationAppService = simulationAppService;
            _partnerAppService = partnerAppService;
        }

        [Route("Simulate")]
        [AllowAnonymous]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<IOperationResponse> Simulate(SimulationViewModel simulation)
        {
            try
            {
                var ret = await _simulationAppService.Simulate(simulation);
                return new OperationResponse(true,"Ok", ret);
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }

        [Route("GetAllSimulations")]
        [Authorize]
        [HttpGet]
        public async Task<IOperationResponse> GetAllSimulations()
        {
            try
            {
                var simulations = await _simulationAppService.GetAllSimulations();
                var totalSimulations = TotalSimulations(simulations);

                var ret = new
                {
                    Simulations = simulations,
                    Total = totalSimulations
                };

                return new OperationResponse(true, "Ok", ret);
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }

        [Route("GetAllPartnersWithSimulations")]
        [Authorize]
        [HttpGet]
        public async Task<IOperationResponse> GetAllPartnersWithSimulations()
        {
            try
            {
                var ret = await _partnerAppService.GetAllWithSimulations();
                
                return new OperationResponse(true, "", ret);
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }

        [Route("GetSummaryStatistics")]
        [Authorize]
        [HttpGet]
        public async Task<IOperationResponse> GetSummaryStatistics(Guid? partnerId)
        {
            try
            {
                var ret = await _simulationAppService.GetSummaryStatistics(partnerId);
                return new OperationResponse(true, "", ret);
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }

        private int TotalSimulations(IEnumerable<GeneralStatisticViewModel> simulations)
        {
            var total = 0;

            foreach(var item in simulations)
            {
                total += item.SimulationTimes;
            }

            return total;
        }
    }
}