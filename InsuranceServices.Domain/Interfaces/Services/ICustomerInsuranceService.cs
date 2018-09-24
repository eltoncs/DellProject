using InsuranceServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Services
{
    public interface ICustomerInsuranceService : IDisposable
    {
        Task<CustomerInsurance> Save(CustomerInsurance customer, Guid id);
        Task<IEnumerable<CustomerInsurance>> GetAll();
        Task<CustomerInsurance> GetById(Guid id);
        Task Remove(Guid Id);
    }
}
