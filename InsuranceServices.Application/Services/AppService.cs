using InsuranceServices.Infra.Data.UnitOfWork;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Services
{
    public class AppService
    {
        private readonly IUnitOfWork _wow;

        public AppService(IUnitOfWork wow)
        {
            _wow = wow;
        }

        protected async Task Commit()
        {
            await _wow.Commit();
        }
    }
}
