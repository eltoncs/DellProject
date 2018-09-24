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
    public class InsuranceTypeAppService : AppService, IInsuranceTypeAppService
    {
        private readonly IInsuranceTypeService _InsuranceTypeService;

        public InsuranceTypeAppService(IInsuranceTypeService InsuranceTypeService, IUnitOfWork wow) : base(wow)
        {
            _InsuranceTypeService = InsuranceTypeService;
        }

        public void Dispose()
        {
            _InsuranceTypeService.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<InsuranceTypeViewModel>> GetAll()
        {
            var InsuranceType = await _InsuranceTypeService.GetAll();
            return Mapper.Map<IEnumerable<InsuranceTypeViewModel>>(InsuranceType);
        }

        public async Task<InsuranceTypeViewModel> GetById(Guid id)
        {
            var InsuranceType = await _InsuranceTypeService.GetById(id);
            return Mapper.Map<InsuranceTypeViewModel>(InsuranceType);
        }

        public async Task<InsuranceTypeViewModel> GetByName(string name)
        {
            var InsuranceType = await _InsuranceTypeService.GetByName(name);
            return Mapper.Map<InsuranceTypeViewModel>(InsuranceType);
        }

        public async Task<IEnumerable<InsuranceTypeCheckListViewModel>> GetCheckList()
        {
            var InsuranceType = await _InsuranceTypeService.GetAll();
            return Mapper.Map<IEnumerable<InsuranceTypeCheckListViewModel>>(InsuranceType);
        }

        public async Task<IEnumerable<string>> GetComboList()
        {
            var InsuranceType = await _InsuranceTypeService.GetComboList();
            return InsuranceType;
        }

        public async Task Remove(Guid id)
        {
            await _InsuranceTypeService.Remove(id);
            await Commit();
        }

        public async Task<InsuranceTypeViewModel> Save(InsuranceTypeViewModel InsuranceTypeViewModel)
        {
            var InsuranceType = Mapper.Map<InsuranceType>(InsuranceTypeViewModel);
            var InsuranceTypeReturn = await _InsuranceTypeService.Save(InsuranceType, InsuranceType.Id);

            if (InsuranceType.ValidationResult.IsValid)
            {
                await Commit();
            }else
            {
                Dispose();
            }

            return Mapper.Map<InsuranceTypeViewModel>(InsuranceTypeReturn);
        }        
    }
}

