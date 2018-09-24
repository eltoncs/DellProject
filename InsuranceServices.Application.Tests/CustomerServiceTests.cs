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
    public class CustomerServiceTests
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly ICustomerInsuranceAppService _customerInsuranceAppService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerInsuranceService _customerInsuranceService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerInsuranceRepository _customerInsuranceRepository;
        private readonly ISContext _context;
        private readonly IUnitOfWork _wow;

        public CustomerServiceTests()
        {
            _context = new ISContext();
            _wow = new UnitOfWork(_context);
            _customerRepository = new CustomerRepository(_context);
            _customerInsuranceRepository = new CustomerInsuranceRepository(_context);
            _customerService = new CustomerService(_customerRepository);
            _customerInsuranceService = new CustomerInsuranceService(_customerInsuranceRepository);
            _customerInsuranceAppService = new CustomerInsuranceAppService(_customerInsuranceService, _wow);
            _customerAppService = new CustomerAppService(_customerService, _customerInsuranceService, _wow);
        }

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            AutoMapperConfig.RegisterMappings();
        }

        [TestMethod]
        public async Task SaveNewCustomer()
        {          
            var newId = Guid.NewGuid();
            var customer = new CustomerViewModel()
            {
                Address = "Some address",
                Name = "Customer_" + Guid.NewGuid(),
                PhoneNumber = "99999999999"
            };

            var result = await _customerAppService.Save(customer);           

            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new customer");
        }

        [TestMethod]
        public async Task SaveNewCustomerTwiceBreakRule()
        {
            await _customerAppService.Remove(new Guid("90EA5BB9-FB01-4023-BA7F-2319C8B4C7B9"));

            var newId = Guid.NewGuid();
            var customer = new CustomerViewModel()
            {
                Id = new Guid("90EA5BB9-FB01-4023-BA7F-2319C8B4C7B9"),
                Address = "Some address",
                Name = "Repeated Customer",
                PhoneNumber = "99999999999"
            };

            var result = await _customerAppService.Save(customer);

            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new customer");

            newId = Guid.NewGuid();
            customer = new CustomerViewModel()
            {
                Address = "Some address",
                Name = "Repeated Customer",
                PhoneNumber = "99999999999"
            };

            result = await _customerAppService.Save(customer);
            Assert.AreNotEqual(true, result.ValidationResult.IsValid, "Error while trying to save a customer with the same name");
        }

        [TestMethod]
        public async Task RemoveCustomer()
        {
            var name = "Deleted Customer";
            var found = await _customerAppService.GetByName(name);
            if (found != null) await _customerAppService.Remove(found.Id);

            var customer = new CustomerViewModel()
            {
                Address = "Some address",
                Name = name,
                PhoneNumber = "99999999999"
            };

            var result = await _customerAppService.Save(customer);
            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new customer");           
        }

        [TestMethod]
        public async Task findCustomerById()
        {
            var newId = new Guid("90ea5bb9-fb01-4023-ba7f-2319c8b4c7b9");

            var customer = new CustomerViewModel()
            {
                Id = newId,
                Address = "Some address",
                Name = "Found Customer",
                PhoneNumber = "99999999999"
            };

            var result = await _customerAppService.Save(customer);
            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new customer");

           var result2 = await _customerAppService.GetById(customer.Id);
            Assert.AreNotEqual(null, result2, "Just saved customer was not found");
        }

        [TestMethod]
        public async Task findCustomerByName()
        {
            var newId = new Guid("90ea5bb9-fb01-4023-ba7f-2319c8b4c7b9");

            var customer = new CustomerViewModel()
            {
                Id = newId,
                Address = "Some address",
                Name = "Found Customer",
                PhoneNumber = "99999999999"
            };

            var result = await _customerAppService.Save(customer);
            Assert.AreEqual(true, result.ValidationResult.IsValid, "Error while trying to save a new customer");

            var result2 = await _customerAppService.GetByName(customer.Name);
            Assert.AreNotEqual(null, result2, "Just saved customer was not found");
        }
    }
}
