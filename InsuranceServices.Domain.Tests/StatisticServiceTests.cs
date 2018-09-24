using InsuranceServices.Domain.Entities;
using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Services;
using InsuranceServices.Infra.Data.Context;
using InsuranceServices.Infra.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Tests
{
    [TestClass]
    public class StatisticServiceTests
    {
        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private IStatisticRepository _statisticRepository = new StatisticRepository(new ISContext());
        private IInsuranceTypeRepository _insuranceTypeRepository = new InsuranceTypeRepository(new ISContext());
        private IPartnerRepository _partnerRepository = new PartnerRepository(new ISContext());

        [TestMethod]
        public async Task RemoveStatistic()
        {
            int result = 0;
            var statisticService = new StatisticService(_statisticRepository);
            var newId = Guid.NewGuid();

            await SaveNewInsuranceType();//Assure the existence of at least one insurance type (House)
            await SaveNewPartner();//Assure the existence of at least one partner (First Partner)

            var insuranceType = await _insuranceTypeRepository.GetByName("House");
            var partner = await _partnerRepository.GetByName("First Partner");

            var statistic = new Statistic()
            {
                Id = newId,
                InsuranceTypeId = insuranceType.Id,
                PartnerId = partner.Id
            };

            result = await SaveStatistic(statistic);

            result = 0;
            statisticService = new StatisticService(_statisticRepository);
            await statisticService.Remove(newId);
            result = await _statisticRepository.SaveChanges();

            Assert.AreEqual(1, result, "Error while trying to save a new statistic");
        }

        
        [TestMethod]
        public async Task SaveNewStatistic()
        {
            int result = 0;
            var statisticService = new StatisticService(_statisticRepository);
            var newId = Guid.NewGuid();

            await SaveNewInsuranceType();//Assure the existence of at least one insurance type (House)
            await SaveNewPartner();//Assure the existence of at least one partner (First Partner)

            var insuranceType = await _insuranceTypeRepository.GetByName("House");
            var partner = await _partnerRepository.GetByName("First Partner");

            var statistic = new Statistic()
            {
                Id = newId,
                InsuranceTypeId = insuranceType.Id,
                PartnerId = partner.Id
            };

            result = await SaveStatistic(statistic);

            Assert.AreEqual(1, result, "Error while trying to save a new statistic");
        }

        
        [TestMethod]
        public async Task GetAllStatistics()
        {
            var statisticService = new StatisticService(_statisticRepository);
            var newId = Guid.NewGuid();
            await SaveNewInsuranceType();//Assure the existence of at least one insurance type (House)
            await SaveNewPartner();//Assure the existence of at least one partner (First Partner)

            var insuranceType = await _insuranceTypeRepository.GetByName("House");
            var partner = await _partnerRepository.GetByName("First Partner");

            var statistic = new Statistic()
            {
                Id = newId,
                InsuranceTypeId = insuranceType.Id,
                PartnerId = partner.Id
            };

            await SaveStatistic(statistic);

            statisticService = new StatisticService(_statisticRepository);
            var found = await statisticService.GetAll();

            Assert.AreNotEqual(null, found, "Returned null when expected a list of statistics");
            Assert.IsTrue(found.ToList().Count > 0);
        }

        private async Task<int> SaveStatistic(Statistic Statistic)
        {
            var result = 0;
            var statisticService = new StatisticService(_statisticRepository);
            await statisticService.Save(Statistic, Statistic.Id);

            result = await _statisticRepository.SaveChanges();
            return result;
        }

        private async Task SaveNewInsuranceType()
        {
            var insuranceTypeService = new InsuranceTypeService(_insuranceTypeRepository);

            var found = await insuranceTypeService.GetByName("House");
            if (found != null) return;

            var newId = Guid.NewGuid();
            var insuranceType = new InsuranceType()
            {
                Id = newId,
                Name = "House",
                BaseIndexValue = (decimal)0.002,
                UsageYearIndexValue = 0,
                MaxYearUsage = 0,
                FixedIndexValue = 0,
                ExtraForSize = (decimal)0.00001
            };

            await _insuranceTypeRepository.Save(insuranceType, newId);
            await _insuranceTypeRepository.SaveChanges();
        }

        private async Task SaveNewPartner()
        {
            var partnerService = new PartnerService(_partnerRepository);

            var found = await partnerService.GetByName("First Partner");
            if (found != null) return;

            var newId = Guid.NewGuid();
            var partner = new Partner()
            {
                Id = newId,
                Name = "First Partner"
            };

            await _partnerRepository.Save(partner, newId);
            await _partnerRepository.SaveChanges();
        }
    }
}
