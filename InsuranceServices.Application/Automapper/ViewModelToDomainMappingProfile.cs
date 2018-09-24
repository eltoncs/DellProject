using AutoMapper;
using InsuranceServices.Application.ViewModels;
using InsuranceServices.Domain.Entities;

namespace InsuranceServices.Application.Automapper
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<PartnerViewModel, Partner>();
            CreateMap<InsuranceTypeViewModel, InsuranceType>();
            CreateMap<CustomerInsuranceViewModel, CustomerInsurance>();
            CreateMap<InsuranceTypeCheckListViewModel, InsuranceType>();
            CreateMap<InsuranceTypeCheckListViewModel, CustomerInsurance>();
        }
    }
}
