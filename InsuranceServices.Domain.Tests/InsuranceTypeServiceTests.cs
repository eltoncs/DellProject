using InsuranceServices.Domain.Entities;
using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Domain.Services;
using InsuranceServices.Infra.Data.Context;
using InsuranceServices.Infra.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Tests
{
    [TestClass]
    public class InsuranceTypeServiceTests
    {
        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private IInsuranceTypeRepository _insuranceTypeRepository = new InsuranceTypeRepository(new ISContext());

        [TestMethod]
        public async Task RemoveInsuranceType()
        {
            int result = 0;
            var newId = Guid.NewGuid();
            var insuranceTypeService = new InsuranceTypeService(_insuranceTypeRepository);
            var insuranceType = new InsuranceType()
            {
                Id = newId,
                Name = "Car",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0                         
            };

            result = await SaveInsuranceType(insuranceType);

            result = 0;
            insuranceTypeService = new InsuranceTypeService(_insuranceTypeRepository);
            await insuranceTypeService.Remove(newId);

            if (insuranceTypeService.Validation.IsValid)
            {
                result = await _insuranceTypeRepository.SaveChanges();
            }

            Assert.AreEqual(1, result, "Error while trying to save a new insurance type");
        }

        [TestMethod]
        public async Task SaveInsuranceTypeTwiceBreakRule()
        {
            int result = 0;
            var insuranceTypeService = new InsuranceTypeService(_insuranceTypeRepository);
            var newId = Guid.NewGuid();
            var insuranceType = new InsuranceType()
            {
                Id = newId,
                Name = "Motorcycle",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0
            };

            result = await SaveInsuranceType(insuranceType);

            result = 0;
            newId = Guid.NewGuid();
            insuranceType = new InsuranceType()
            {
                Id = newId,
                Name = "Motorcycle",
                BaseIndexValue = (decimal)0.06,
                UsageYearIndexValue = (decimal)0.05,
                MaxYearUsage = 20,
                FixedIndexValue = (decimal)0.15,
                ExtraForSize = 0
            };

            result = await SaveInsuranceType(insuranceType);

            Assert.AreNotEqual(1, result, "Duplicate record in InsuranceType");
        }
                
        [TestMethod]
        public async Task GetAllInsuranceTypes()
        {
            var insuranceTypeService = new InsuranceTypeService(_insuranceTypeRepository);
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

            await SaveInsuranceType(insuranceType);

            insuranceTypeService = new InsuranceTypeService(_insuranceTypeRepository);
            var found = await insuranceTypeService.GetAll();

            Assert.AreNotEqual(null, found, "Returned null when expected a list of insurance types");
            Assert.IsTrue(found.ToList().Count > 0);
        }

        private async Task<int> SaveInsuranceType(InsuranceType insuranceType)
        {
            var result = 0;
            var insuranceTypeService = new InsuranceTypeService(_insuranceTypeRepository);
            await insuranceTypeService.Save(insuranceType, insuranceType.Id);

            if (insuranceTypeService.Validation.IsValid)
            {
                result = await _insuranceTypeRepository.SaveChanges();
            }

            return result;
        }
    }
}