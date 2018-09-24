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
    public class CustomerServiceTests
    {
        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private ICustomerRepository _customerRepository = new CustomerRepository(new ISContext());

        [TestMethod]
        public async Task RemoveCustomer()
        {
            int result = 0;
            var customerService = new CustomerService(_customerRepository);
            var newId = Guid.NewGuid();
            var customer = new Customer()
            {
                Id = newId,
                Address = "Some address",
                Name = "Customer to be deleted_" + Guid.NewGuid(),
                PhoneNumber = "99999999999"
            };

            result = await SaveCustomer(customer);
           
            result = 0;
            customerService = new CustomerService(_customerRepository);
            await customerService.Remove(newId);

            if (customerService.Validation.IsValid)
            {
                result = await _customerRepository.SaveChanges();
            }

            Assert.AreEqual(1, result, "Error while trying to save a new customer");
        }

        [TestMethod]
        public async Task SaveCustomerTwiceBreakRule()
        {
            int result = 0;
            var customerService = new CustomerService(_customerRepository);
            var newId = Guid.NewGuid();
            var customer = new Customer()
            {
                Id = newId,
                Address = "Some address",
                Name = "Repeated Customer",
                PhoneNumber = "99999999999"
            };

            result = await SaveCustomer(customer);

            result = 0;
            newId = Guid.NewGuid();
            customer = new Customer()
            {
                Id = newId,
                Address = "Some address",
                Name = "Repeated Customer",
                PhoneNumber = "99999999999"
            };

            result = await SaveCustomer(customer);

            Assert.AreNotEqual(1, result, "Duplicate record in Customers");
        }

        [TestMethod]
        public async Task SaveNewCustomer()
        {
            int result = 0;
            var customerService = new CustomerService(_customerRepository);
            var newId = Guid.NewGuid();
            var customer = new Customer()
            {
                Id = newId,
                Address = "Some address",
                Name = "Customer_" + Guid.NewGuid(),
                PhoneNumber = "99999999999"
            };

            result = await SaveCustomer(customer);

            Assert.AreEqual(1, result, "Error while trying to save a new customer");
        }

        [TestMethod]
        public async Task FindCustomerById()
        {
            var customerService = new CustomerService(_customerRepository);
            var newId = new Guid("90ea5bb9-fb01-4023-ba7f-2319c8b4c7b9");
            var customer = new Customer()
            {
                Id = newId,
                Address = "Some address",
                Name = "Find me",
                PhoneNumber = "99999999999"
            };

            await SaveCustomer(customer);

            customerService = new CustomerService(_customerRepository);
            Customer found = await customerService.GetById(newId);

            Assert.AreNotEqual(null, found, "Just saved customer was not found");
        }

        [TestMethod]
        public async Task FindCustomerByName()
        {
            var customerService = new CustomerService(_customerRepository);
            var newId = new Guid("90ea5bb9-fb01-4023-ba7f-2319c8b4c7b9");
            var customer = new Customer()
            {
                Id = newId,
                Address = "Some address",
                Name = "Find me",
                PhoneNumber = "99999999999"
            };

            await SaveCustomer(customer);

            customerService = new CustomerService(_customerRepository);
            Customer found = await customerService.GetByName("Find me");

            Assert.AreNotEqual(null, found, "Just saved customer was not found");
        }

        [TestMethod]
        public async Task GetAllCustomers()
        {
            var customerService = new CustomerService(_customerRepository);
            var newId = Guid.NewGuid();
            var customer = new Customer()
            {
                Id = newId,
                Address = "Some address",
                Name = "One customer at least",
                PhoneNumber = "99999999999"
            };

            await SaveCustomer(customer);

            customerService = new CustomerService(_customerRepository);
            var found = await customerService.GetAll();

            Assert.AreNotEqual(null, found, "Returned null when expected a list of customers");
            Assert.IsTrue(found.ToList().Count > 0);
        }

        private async Task<int> SaveCustomer(Customer customer)
        {
            var result = 0;
            var customerService = new CustomerService(_customerRepository);
            var r = await customerService.Save(customer, customer.Id);

            if (customerService.Validation.IsValid)
            {
                result = await _customerRepository.SaveChanges();
            }

            return result;
        }
    }
}
