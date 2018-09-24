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
    public class CustomerAppService: AppService, ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerInsuranceService _customerInsuranceService;

        public CustomerAppService(ICustomerService customerService, 
                                  ICustomerInsuranceService customerInsuranceService,
                                  IUnitOfWork wow) : base(wow)
        {
            _customerService = customerService;
            _customerInsuranceService = customerInsuranceService;
        }

        public void Dispose()
        {
            _customerService.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            var customer = await _customerService.GetAll();
            return Mapper.Map<IEnumerable<CustomerViewModel>>(customer);
        }

        public async Task<CustomerViewModel> GetById(Guid id)
        {
            var customer = await _customerService.GetById(id);
            return Mapper.Map<CustomerViewModel>(customer);
        }

        public async Task<CustomerViewModel> GetByName(string name)
        {
            var customer = await _customerService.GetByName(name);
            return Mapper.Map<CustomerViewModel>(customer);
        }

        public async Task Remove(Guid id)
        {
            var customer = await _customerService.GetById(id);
            if (customer == null) throw new KeyNotFoundException($"customer id {id} not found");

            await DeleteCustomerInsurances(id);
            await _customerService.Remove(id);
            await Commit();
        }

        public async Task<CustomerViewModel> Save(CustomerViewModel customerViewModel)
        {
            var customer = Mapper.Map<Customer>(customerViewModel);
            customer.Insurances = null;
            await DeleteCustomerInsurances(customer.Id);
            var customerReturn = await _customerService.Save(customer, customer.Id);

            if (customer.ValidationResult.IsValid)
            {
                if (customerViewModel.Insurances != null)
                {
                    foreach (var item in customerViewModel.Insurances)
                    {
                        var customerInsurance = new CustomerInsurance()
                        {
                            Id = Guid.NewGuid(),
                            CustomerId = customerReturn.Id,
                            InsuranceTypeId = item.Id
                        };

                        var customerInsuranceRet = await _customerInsuranceService.Save(customerInsurance, customerInsurance.Id);
                    }
                }               
            }

            await Commit();
            return Mapper.Map<CustomerViewModel>(customerReturn);
        }

        public async Task DeleteCustomerInsurances(Guid CustomerId) 
        {
            var exit = false;
            var customer = await _customerService.GetById(CustomerId);
            if (customer == null) return;
            if (customer.Insurances.Count == 0) return;

            while (!exit)
            {
                foreach (var item in customer.Insurances)
                {
                    await _customerInsuranceService.Remove(item.Id);
                    exit = (customer.Insurances.Count == 0);
                    break;
                }
            }
           
        }
    }
}
