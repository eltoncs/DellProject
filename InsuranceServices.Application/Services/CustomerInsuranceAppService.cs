using InsuranceServices.Application.Interfaces;
using InsuranceServices.Domain.Interfaces.Services;
using InsuranceServices.Infra.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InsuranceServices.Application.ViewModels;
using AutoMapper;
using InsuranceServices.Domain.Entities;

namespace InsuranceServices.Application.Services
{
    public class CustomerInsuranceAppService : AppService, ICustomerInsuranceAppService
    {
        private readonly ICustomerInsuranceService _customerInsuranceService;

        public CustomerInsuranceAppService(ICustomerInsuranceService customerInsuranceService, IUnitOfWork wow) : base(wow)
        {
            _customerInsuranceService = customerInsuranceService;
        }
        public void Dispose()
        {
            _customerInsuranceService.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<CustomerInsuranceViewModel>> GetAll()
        {
            var customerInsurances = await _customerInsuranceService.GetAll();
            return Mapper.Map<IEnumerable<CustomerInsuranceViewModel>>(customerInsurances);
        }

        public async Task<CustomerInsuranceViewModel> GetById(Guid id)
        {
            var customerInsurance = await _customerInsuranceService.GetById(id);
            return Mapper.Map<CustomerInsuranceViewModel>(customerInsurance);
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerInsuranceViewModel> Save(CustomerInsuranceViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
