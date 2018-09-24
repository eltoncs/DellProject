using InsuranceServices.Domain.Interfaces.Validations;
using InsuranceServices.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Specifications.Customer
{
    public class UniqueNameSpecification : ISpecification<Entities.Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public UniqueNameSpecification(ICustomerRepository customerrepository)
        {
            _customerRepository = customerrepository;
        }

        public async Task<bool> IsSatisfiedBy(Entities.Customer entity)
        {
            var currentCustomer = await _customerRepository.GetByName(entity.Name);
            if (currentCustomer == null) return true;

            return (currentCustomer.Id == entity.Id);
        }
    }
}
