using InsuranceServices.Domain.Interfaces.Validations;
using InsuranceServices.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Specifications.Partner
{
    public class UniqueNameSpecification : ISpecification<Entities.Partner>
    {
        private readonly IPartnerRepository _partnerRepository;

        public UniqueNameSpecification(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<bool> IsSatisfiedBy(Entities.Partner entity)
        {
            var currentPartner = await _partnerRepository.GetByName(entity.Name);
            if (currentPartner == null) return true;

            return (currentPartner.Id == entity.Id);
        }
    }
}
