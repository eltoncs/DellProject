using InsuranceServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Services
{
    public interface IInsuranceTypeService: IDisposable
    {
        Task<InsuranceType> Save(InsuranceType insuranceType, Guid id);
        Task<InsuranceType> GetById(Guid id);
        Task<InsuranceType> GetByName(string name);
        Task<IEnumerable<InsuranceType>> GetAll();
        Task<IEnumerable<string>> GetComboList();
        Task<IEnumerable<InsuranceType>> GetCheckList();
        Task Remove(Guid Id);
    }
}
