using System.Threading.Tasks;

namespace InsuranceServices.Infra.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
