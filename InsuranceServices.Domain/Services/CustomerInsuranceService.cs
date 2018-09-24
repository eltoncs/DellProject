using InsuranceServices.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using InsuranceServices.Domain.Entities;
using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Interfaces.Validations;
using InsuranceServices.Domain.Specifications.Customer;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Services
{
    public class CustomerInsuranceService : IDomainValidation, ICustomerInsuranceService
    {
        private readonly ICustomerInsuranceRepository _customerInsuranceRepository;

        public CustomerInsuranceService(ICustomerInsuranceRepository customerInsuranceRepository)
        {
            _customerInsuranceRepository = customerInsuranceRepository;
        }

        public void Dispose()
        {
            _customerInsuranceRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<CustomerInsurance>> GetAll()
        {
            return await _customerInsuranceRepository.GetAll();
        }

        public async Task<CustomerInsurance> GetById(Guid id)
        {
            return await _customerInsuranceRepository.GetById(id);
        }

        public async Task Remove(Guid id)
        {
            await _customerInsuranceRepository.Remove(id);
        }

        public async Task<CustomerInsurance> Save(CustomerInsurance customer, Guid id)
        {
            return await _customerInsuranceRepository.Save(customer, id);
        }
    }
}
