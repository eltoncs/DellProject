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
    public class CustomerService : IDomainValidation, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

    
        public void Dispose()
        {
            _customerRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customerRepository.GetAll();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task<Customer> GetByName(string name)
        {
            return await _customerRepository.GetByName(name);
        }

        public async Task Remove(Guid id)
        {
            await _customerRepository.Remove(id);
        }

        public async Task<Customer> Save(Customer customer, Guid id)
        {
            await ValidateSave(customer);
            customer.ValidationResult = _validation;

            if (!_validation.IsValid)
            {
                return customer;
            }

            return await _customerRepository.Save(customer, id);
        }

        private async Task ValidateSave(Entities.Customer customer)
        {
            var duplicatedName = new UniqueNameSpecification(_customerRepository);
            var valid = await duplicatedName.IsSatisfiedBy(customer);
            if (!valid) _validation.BrokenRules.Add($"There is already a customer called {customer.Name}");
        }
    }
}
