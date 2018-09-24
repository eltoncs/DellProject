using InsuranceServices.Application.Interfaces;
using InsuranceServices.Domain.Interfaces.Services;
using InsuranceServices.Infra.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceServices.Application.ViewModels;
using System.Linq.Expressions;
using AutoMapper;
using InsuranceServices.Domain.Entities;

namespace InsuranceServices.Application.Services
{
    public class StatisticAppService : AppService, IStatisticAppService
    {
        private readonly IStatisticService _StatisticService;

        public StatisticAppService(IStatisticService StatisticService, IUnitOfWork wow) : base(wow)
        {
            _StatisticService = StatisticService;
        }
             
        public void Dispose()
        {
            _StatisticService.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<StatisticViewModel>> GetAll()
        {
            var Statistic = await _StatisticService.GetAll();
            return Mapper.Map<IEnumerable<StatisticViewModel>>(Statistic);
        }

        public Task<StatisticViewModel> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(Guid id)
        {
            await _StatisticService.Remove(id);
            await Commit();
        }

        public async Task<StatisticViewModel> Save(StatisticViewModel StatisticViewModel)
        {
            var Statistic = Mapper.Map<Statistic>(StatisticViewModel);
            var StatisticReturn = await _StatisticService.Save(Statistic, Statistic.Id);

            await Commit();

            return Mapper.Map<StatisticViewModel>(StatisticReturn);
        }
    }
}
