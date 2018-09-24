using InsuranceServices.Application.Interfaces;
using InsuranceServices.Domain.Interfaces.Services;
using InsuranceServices.Infra.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InsuranceServices.Application.ViewModels;
using AutoMapper;
using InsuranceServices.Domain.Entities;

namespace InsuranceServices.Application.Services
{
    public class PartnerAppService : AppService, IPartnerAppService
    {
        private readonly IPartnerService _partnerService;

        public PartnerAppService(IPartnerService partnerService, IUnitOfWork wow) : base(wow)
        {
            _partnerService = partnerService;
        }

        public void Dispose()
        {
            _partnerService.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<PartnerViewModel>> GetAll()
        {
            var partners = await _partnerService.GetAll();
            return Mapper.Map<IEnumerable<PartnerViewModel>>(partners);
        }

        public async Task<IEnumerable<PartnerViewModel>> GetAllWithSimulations()
        {
            var partners = await _partnerService.GetAllWithSimulations();
            return Mapper.Map<IEnumerable<PartnerViewModel>>(partners);
        }

        public async Task<PartnerViewModel> GetById(Guid id)
        {
            var Partner = await _partnerService.GetById(id);
            return Mapper.Map<PartnerViewModel>(Partner);
        }

        public async Task<PartnerViewModel> GetByName(string name)
        {
            var Partner = await _partnerService.GetByName(name);
            return Mapper.Map<PartnerViewModel>(Partner);
        }

        public async Task Remove(Guid id)
        {
            var partner = await _partnerService.GetById(id);
            if (partner == null) throw new KeyNotFoundException($"The customer id {id} was not found");

            if (partner.Simulations.Count > 0) throw new Exception("Can not remove the customer because it has simulations");

            await _partnerService.Remove(id);
            await Commit();
        }

        public async Task<PartnerViewModel> Save(PartnerViewModel PartnerViewModel)
        {
            var Partner = Mapper.Map<Partner>(PartnerViewModel);
            var PartnerReturn = await _partnerService.Save(Partner, Partner.Id);

            if (Partner.ValidationResult.IsValid)
            {
                await Commit();
            }

            return Mapper.Map<PartnerViewModel>(PartnerReturn);
        }
    }
}
