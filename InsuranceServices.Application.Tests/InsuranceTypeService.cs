using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using InsuranceServices.Application.Services;
using InsuranceServices.Domain.Services;
using InsuranceServices.Domain.Interfaces.Services;
using InsuranceServices.Infra.Data.Repository;
using InsuranceServices.Infra.Data.Context;
using InsuranceServices.Infra.Data.UnitOfWork;
using InsuranceServices.Application.Interfaces;
using InsuranceServices.Application.ViewModels;
using InsuranceServices.Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using InsuranceServices.Application.Automapper;

namespace InsuranceServices.Application.Tests
{
    [TestClass]
    public class InsuranceTypeServiceTests
    {
        private readonly IInsuranceTypeAppService _InsuranceTypeAppService;
        private readonly IInsuranceTypeService _InsuranceTypeService;
        private readonly IInsuranceTypeRepository _InsuranceTypeRepository;
        private readonly ISContext _context;
        private readonly IUnitOfWork _wow;

        public InsuranceTypeServiceTests()
        {
            _context = new ISContext();
            _wow = new UnitOfWork(_context);
            _InsuranceTypeRepository = new InsuranceTypeRepository(_context);
            _InsuranceTypeService = new InsuranceTypeService(_InsuranceTypeRepository);
            _InsuranceTypeAppService = new InsuranceTypeAppService(_InsuranceTypeService, _wow);
        }

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            AutoMapperConfig.RegisterMappings();
        }

        [TestMethod]
        public async Task SaveNewInsuranceType()
        {
            var found = await _InsuranceTypeAppService.GetByName("Car");
            if (found != null) await _InsuranceTypeAppService.Remove(found.Id);

            var newId = Guid.NewGuid();
            var InsuranceType = new InsuranceTypeViewModel()
            {
                Id = newId,
                Name = "Car",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0
            };

            var result = await _InsuranceTypeAppService.Save(InsuranceType);

            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new InsuranceType");
        }

        [TestMethod]
        public async Task SaveNewInsuranceTypeTwiceBreakRule()
        {
            var found = await _InsuranceTypeAppService.GetByName("Car");
            if (found != null) await _InsuranceTypeAppService.Remove(found.Id);

            var newId = Guid.NewGuid();
            var InsuranceType = new InsuranceTypeViewModel()
            {
                Id = new Guid("90EA5BB9-FB01-4023-BA7F-2319C8B4C7B9"),
                Name = "Car",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0
            };

            var result = await _InsuranceTypeAppService.Save(InsuranceType);

            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new InsuranceType");

            newId = Guid.NewGuid();
            InsuranceType = new InsuranceTypeViewModel()
            {
                Name = "Car",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0
            };

            result = await _InsuranceTypeAppService.Save(InsuranceType);
            Assert.AreNotEqual(true, result.ValidationResult.IsValid, "Error while trying to save a InsuranceType with the same name");
        }

        [TestMethod]
        public async Task RemoveInsuranceType()
        {
            var InsuranceType = new InsuranceTypeViewModel()
            {
                Name = "Deleted Car",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0
            };

            var result = await _InsuranceTypeAppService.Save(InsuranceType);
            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new InsuranceType");

            await _InsuranceTypeAppService.Remove(InsuranceType.Id);
            var result2 = await _InsuranceTypeAppService.GetById(InsuranceType.Id);
            Assert.AreEqual(null, result2, "Error while trying to delete a InsuranceType");
        }

        [TestMethod]
        public async Task FindInsuranceTypeById()
        {
            var found = await _InsuranceTypeAppService.GetByName("Car");
            if (found != null) await _InsuranceTypeAppService.Remove(found.Id);

            var newId = new Guid("90ea5bb9-fb01-4023-ba7f-2319c8b4c7b9");

            var InsuranceType = new InsuranceTypeViewModel()
            {
                Id = newId,
                Name = "Car",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0
            };

            var result = await _InsuranceTypeAppService.Save(InsuranceType);
            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new InsuranceType");

            var result2 = await _InsuranceTypeAppService.GetById(InsuranceType.Id);
            Assert.AreNotEqual(null, result2, "Just saved InsuranceType was not found");
        }

        [TestMethod]
        public async Task findInsuranceTypeByName()
        {
            var newId = new Guid("90ea5bb9-fb01-4023-ba7f-2319c8b4c7b9");

            var InsuranceType = new InsuranceTypeViewModel()
            {
                Id = newId,
                Name = "Car",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0
            };

            var result = await _InsuranceTypeAppService.Save(InsuranceType);
            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new InsuranceType");

            var result2 = await _InsuranceTypeAppService.GetByName(InsuranceType.Name);
            Assert.AreNotEqual(null, result2, "Just saved InsuranceType was not found");
        }
    }
}
