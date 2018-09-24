using InsuranceServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Services
{
    public interface ICustomerService : IDisposable
    {
        Task<Customer> Save(Customer customer, Guid id);
        Task<Customer> GetById(Guid id);
        Task<Customer> GetByName(string name);
        Task<IEnumerable<Customer>> GetAll();
        Task Remove(Guid Id);        
    }
}
