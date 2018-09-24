using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Validations
{
    public interface ISpecification<in T>
    {
        Task<bool> IsSatisfiedBy(T entity);
    }
}
