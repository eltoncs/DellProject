using InsuranceServices.Application.Interfaces;
using InsuranceServices.Domain.Interfaces.Services;
using InsuranceServices.Infra.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InsuranceServices.Application.ViewModels;
using InsuranceServices.Domain.Entities;
using System.Linq;

namespace InsuranceServices.Application.Services
{
    public class SimulationAppService : AppService, ISimulationAppService
    {
        private readonly IInsuranceTypeService _insuranceTypeService;
        private readonly IStatisticService _statisticService;
        private readonly IPartnerService _partnerService;

        public SimulationAppService(IInsuranceTypeService insuranceTypeService, 
                                    IStatisticService statisticService, 
                                    IPartnerService partnerService, IUnitOfWork wow) : base(wow)
        {
            _insuranceTypeService = insuranceTypeService;
            _statisticService = statisticService;
            _partnerService = partnerService;
        }

        public async Task<decimal> Simulate(SimulationViewModel simulation)
        {
            decimal value = 0;
            var insuranceType = await _insuranceTypeService.GetByName(simulation.PropertyType);

            if (insuranceType == null) throw new KeyNotFoundException($"Property type {simulation.PropertyType} not found");

            if (insuranceType.MaxYearUsage == 0)//Farms and houses
            {
                value = simulation.Value * insuranceType.BaseIndexValue;
                value += simulation.Value * insuranceType.ExtraForSize * simulation.Area;
            }
            else//Cars and motorcycles
            {
                int useTime = DateTime.Now.Year - simulation.ManufacacturedIn;
                if (useTime > insuranceType.MaxYearUsage) return (simulation.Value * insuranceType.FixedIndexValue);

                value = simulation.Value * insuranceType.BaseIndexValue;
                value += simulation.Value * insuranceType.UsageYearIndexValue * useTime;
            }

            await SaveStatistic(simulation.PartnerId, insuranceType.Id);
            return value;
        }

        public async Task<IEnumerable<string>> GetAllInsuranceTypes()
        {
            var ret = await _insuranceTypeService.GetComboList();
            return ret;
        }

        private async Task SaveStatistic(Guid partnerId, Guid insuranceTypeId)
        {
            var statistic = new Statistic()
            {
                Id = Guid.NewGuid(),
                InsuranceTypeId = insuranceTypeId,
                PartnerId = partnerId
            };

            var ret = await _statisticService.Save(statistic, statistic.Id);
            await Commit();
        }

        public async Task<IEnumerable<GeneralStatisticViewModel>> GetAllSimulations()
        {
            var ret = new List<GeneralStatisticViewModel>();
            var partners = await _partnerService.GetAllWithSimulations();

            foreach (var item in partners)
            {
                ret.Add(new GeneralStatisticViewModel()
                {
                    PartnerName = item.Name,
                    SimulationTimes = item.Simulations.Count
                });
            }

            return ret.OrderByDescending(x=> x.SimulationTimes);
        }

        public async Task<IEnumerable<SummaryStatisticViewModel>> GetSummaryStatistics(Guid? partnerId)
        {
            var ret = new List<SummaryStatisticViewModel>();
            IEnumerable<Statistic> statistics;

            if (partnerId == null)
            {
                statistics = await _statisticService.GetAll();
            }
            else
            {
                statistics = await _statisticService.GetByPartner(partnerId.Value);
            }
            
            SummaryStatisticViewModel statistic;

            foreach (var item in statistics)
            {
                statistic = ret.Find(x => x.InsuranceType == item.InsuranceType.Name);
                if (statistic == null)
                {
                    statistic = new SummaryStatisticViewModel() {
                        InsuranceType = item.InsuranceType.Name,
                        Quantity = 1
                    };

                    ret.Add(statistic);
                }
                else
                {
                    statistic.Quantity++;
                }                
            }

            return ret;
        }
    }
}
