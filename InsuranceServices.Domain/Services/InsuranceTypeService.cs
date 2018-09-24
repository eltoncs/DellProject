using InsuranceServices.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using InsuranceServices.Domain.Entities;
using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Interfaces.Validations;
using InsuranceServices.Domain.Specifications.InsuranceType;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Services
{
    public class InsuranceTypeService : IDomainValidation, IInsuranceTypeService
    {
        private readonly IInsuranceTypeRepository _insuranceTypeRepository;

        public InsuranceTypeService(IInsuranceTypeRepository insuranceTypeRepository)
        {
            _insuranceTypeRepository = insuranceTypeRepository;
        }

        public async Task<IEnumerable<InsuranceType>> GetAll()
        {
            return await _insuranceTypeRepository.GetAll();
        }

        public async Task<IEnumerable<string>> GetComboList()
        {
            var ret = await _insuranceTypeRepository.GetComboList();
            return ret;
        }

        public async Task<IEnumerable<InsuranceType>> GetCheckList()
        {
            var ret = await _insuranceTypeRepository.GetAll();
            return ret;
        }

        public async Task<InsuranceType> GetById(Guid id)
        {
            return await _insuranceTypeRepository.GetById(id);
        }

        public async Task<InsuranceType> GetByName(string name)
        {
            return await _insuranceTypeRepository.GetByName(name);
        }

        public async Task Remove(Guid id)
        {
            await _insuranceTypeRepository.Remove(id);
        }

        public async Task<InsuranceType> Save(InsuranceType insuranceType, Guid id)
        {
            await ValidateSave(insuranceType);
            insuranceType.ValidationResult = _validation;

            if (!_validation.IsValid)
            {
                return insuranceType;
            }

            return await _insuranceTypeRepository.Save(insuranceType, id);
        }

        public void Dispose()
        {
            _insuranceTypeRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task ValidateSave(Entities.InsuranceType insuranceType)
        {
            var duplicatedName = new UniqueNameSpecification(_insuranceTypeRepository);
            var valid = await duplicatedName.IsSatisfiedBy(insuranceType);
            if (!valid) _validation.BrokenRules.Add($"There is already a customer called {insuranceType.Name}");
        }

        
    }
}
