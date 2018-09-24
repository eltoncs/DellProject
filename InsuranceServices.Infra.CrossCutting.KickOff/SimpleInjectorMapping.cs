using SimpleInjector;
using InsuranceServices.Application.Interfaces;
using InsuranceServices.Application.Services;
using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Interfaces.Services;
using InsuranceServices.Domain.Services;
using InsuranceServices.Infra.Data.Context;
using InsuranceServices.Infra.Data.Repository;
using InsuranceServices.Infra.Data.UnitOfWork;

namespace InsuranceServices.Infra.CrossCutting.KickOff
{
    public class SimpleInjectorMapping
    {
        public static void Register(Container container)
        {
            //Others (Context/UOW)
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<ISContext>(Lifestyle.Scoped);
           
           //Domain
            container.Register<ICustomerService, CustomerService>(Lifestyle.Scoped);
            container.Register<IInsuranceTypeService, InsuranceTypeService>(Lifestyle.Scoped);
            container.Register<IPartnerService, PartnerService>(Lifestyle.Scoped);
            container.Register<IStatisticService, StatisticService>(Lifestyle.Scoped);
            container.Register<ICustomerInsuranceService, CustomerInsuranceService>(Lifestyle.Scoped);

            //Data 
            container.Register<ICustomerRepository, CustomerRepository>(Lifestyle.Scoped);
            container.Register<IInsuranceTypeRepository, InsuranceTypeRepository>(Lifestyle.Scoped);
            container.Register<IPartnerRepository, PartnerRepository>(Lifestyle.Scoped);
            container.Register<IStatisticRepository, StatisticRepository>(Lifestyle.Scoped);
            container.Register<ICustomerInsuranceRepository, CustomerInsuranceRepository>(Lifestyle.Scoped);
            

            //App
            container.Register<ICustomerAppService, CustomerAppService>(Lifestyle.Scoped);
            container.Register<IPartnerAppService, PartnerAppService>(Lifestyle.Scoped);
            container.Register<ISimulationAppService, SimulationAppService>(Lifestyle.Scoped);
            container.Register<IInsuranceTypeAppService, InsuranceTypeAppService>(Lifestyle.Scoped);
            container.Register<ICustomerInsuranceAppService, CustomerInsuranceAppService>(Lifestyle.Scoped);
        }
    }
}
