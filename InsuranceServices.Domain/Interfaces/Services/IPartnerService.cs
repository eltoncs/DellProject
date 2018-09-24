using InsuranceServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Services
{
    public interface IPartnerService: IDisposable
    {
        Task<Partner> Save(Partner partner, Guid id);
        Task<Partner> GetById(Guid id);
        Task<Partner> GetByName(string name);
        Task<IEnumerable<Partner>> GetAll();
        Task Remove(Guid Id);
        Task<IEnumerable<Partner>> GetAllWithSimulations();
    }
}
