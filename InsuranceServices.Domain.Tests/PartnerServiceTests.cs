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
    public class PartnerServiceTests
    {
        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private IPartnerRepository _partnerRepository = new PartnerRepository(new ISContext());

        [TestMethod]
        public async Task RemovePartner()
        {
            int result = 0;
            var partnerService = new PartnerService(_partnerRepository);
            var newId = Guid.NewGuid();
            var Partner = new Partner()
            {
                Id = newId,
                Name = "Partner to be deleted_" + Guid.NewGuid()
            };

            result = await SavePartner(Partner);

            result = 0;
            partnerService = new PartnerService(_partnerRepository);
            await partnerService.Remove(newId);

            if (partnerService.Validation.IsValid)
            {
                result = await _partnerRepository.SaveChanges();
            }

            Assert.AreEqual(1, result, "Error while trying to save a new partner");
        }

        [TestMethod]
        public async Task SavePartnerTwiceBreakRule()
        {
            int result = 0;
            var partnerService = new PartnerService(_partnerRepository);
            var newId = Guid.NewGuid();
            var partner = new Partner()
            {
                Id = newId,
                Name = "Repeated Partner"
            };

            result = await SavePartner(partner);

            result = 0;
            newId = Guid.NewGuid();
            partner = new Partner()
            {
                Id = newId,
                Name = "Repeated Partner"
            };

            result = await SavePartner(partner);

            Assert.AreNotEqual(1, result, "Duplicate record in Partners");
        }

        [TestMethod]
        public async Task SaveNewPartner()
        {
            int result = 0;
            var partnerService = new PartnerService(_partnerRepository);
            var newId = Guid.NewGuid();
            var partner = new Partner()
            {
                Id = newId,
                Name = "Partner_" + Guid.NewGuid()
            };

            result = await SavePartner(partner);

            Assert.AreEqual(1, result, "Error while trying to save a new partner");
        }

        [TestMethod]
        public async Task FindPartnerById()
        {
            var partnerService = new PartnerService(_partnerRepository);
            var newId = new Guid("90ea5bb9-fb01-4023-ba7f-2319c8b4c7b9");
            var partner = new Partner()
            {
                Id = newId,
                Name = "Find me"
            };

            await SavePartner(partner);

            partnerService = new PartnerService(_partnerRepository);
            Partner found = await partnerService.GetById(newId);

            Assert.AreNotEqual(null, found, "Just saved partner was not found");
        }

        [TestMethod]
        public async Task FindPartnerByName()
        {
            var partnerService = new PartnerService(_partnerRepository);
            var newId = new Guid("90ea5bb9-fb01-4023-ba7f-2319c8b4c7b9");
            var partner = new Partner()
            {
                Id = newId,
                Name = "Find me"
            };

            await SavePartner(partner);

            partnerService = new PartnerService(_partnerRepository);
            Partner found = await partnerService.GetByName("Find me");

            Assert.AreNotEqual(null, found, "Just saved partner was not found");
        }

        [TestMethod]
        public async Task GetAllPartners()
        {
            var partnerService = new PartnerService(_partnerRepository);
            var newId = Guid.NewGuid();
            var partner = new Partner()
            {
                Id = newId,
                Name = "One Partner at least"
            };

            await SavePartner(partner);

            partnerService = new PartnerService(_partnerRepository);
            var found = await partnerService.GetAll();

            Assert.AreNotEqual(null, found, "Returned null when expected a list of partners");
            Assert.IsTrue(found.ToList().Count > 0);
        }

        private async Task<int> SavePartner(Partner partner)
        {
            var result = 0;
            var partnerService = new PartnerService(_partnerRepository);
            await partnerService.Save(partner, partner.Id);

            if (partnerService.Validation.IsValid)
            {
                result = await _partnerRepository.SaveChanges();
            }

            return result;
        }
    }
}
