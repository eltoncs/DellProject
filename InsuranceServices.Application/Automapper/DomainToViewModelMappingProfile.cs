using AutoMapper;
using InsuranceServices.Application.ViewModels;
using InsuranceServices.Domain.Entities;

namespace InsuranceServices.Application.Automapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Partner, PartnerViewModel>();
            CreateMap<InsuranceType, InsuranceTypeViewModel>();
            CreateMap<InsuranceType, InsuranceTypeCheckListViewModel>();
            CreateMap<CustomerInsurance, CustomerInsuranceViewModel>();
            CreateMap<CustomerInsurance, InsuranceTypeCheckListViewModel>();
        }
    }
}
