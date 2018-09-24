using InsuranceServices.Domain.Interfaces.Validations;
using InsuranceServices.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Specifications.InsuranceType
{
    public class UniqueNameSpecification : ISpecification<Entities.InsuranceType>
    {
        private readonly IInsuranceTypeRepository _insuranceTypeRepository;

        public UniqueNameSpecification(IInsuranceTypeRepository insuranceTypeRepository)
        {
            _insuranceTypeRepository = insuranceTypeRepository;
        }

        public async Task<bool> IsSatisfiedBy(Entities.InsuranceType entity)
        {
            var currentInsuranceType = await _insuranceTypeRepository.GetByName(entity.Name);
            if (currentInsuranceType == null) return true;

            return (currentInsuranceType.Id == entity.Id);
        }
    }
}
