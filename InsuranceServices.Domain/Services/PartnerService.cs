using InsuranceServices.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using InsuranceServices.Domain.Entities;
using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Interfaces.Validations;
using InsuranceServices.Domain.Specifications.Partner;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Services
{
    public class PartnerService : IDomainValidation, IPartnerService
    {
        private readonly IPartnerRepository _partnerRepository;

        public PartnerService(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<IEnumerable<Partner>> GetAll()
        {
            return await _partnerRepository.GetAll();
        }

        public async Task<Partner> GetById(Guid id)
        {
            return await _partnerRepository.GetById(id);
        }

        public async Task<Partner> GetByName(string name)
        {
            return await _partnerRepository.GetByName(name);
        }

        public async Task Remove(Guid id)
        {
            await _partnerRepository.Remove(id);
        }

        public async Task<Partner> Save(Partner partner, Guid id)
        {
            await ValidateSave(partner);
            partner.ValidationResult = _validation;

            if (!_validation.IsValid)
            {
                return partner;
            }

            return await _partnerRepository.Save(partner, id);
        }

        public async Task<IEnumerable<Partner>> GetAllWithSimulations()
        {
            return await _partnerRepository.GetAllWithSimulations();
        }

        public void Dispose()
        {
            _partnerRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task ValidateSave(Entities.Partner partner)
        {
            var duplicatedName = new UniqueNameSpecification(_partnerRepository);
            var valid = await duplicatedName.IsSatisfiedBy(partner);
            if (!valid) _validation.BrokenRules.Add($"There is already a partner called {partner.Name}");
        }        
    }
}
