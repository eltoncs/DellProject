using InsuranceServices.Application.Interfaces;
using InsuranceServices.Application.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace InsurranceServices.MVC.Controllers.WebApi
{
    [RoutePrefix("Api/CustomerService")]
    public class CustomerServiceController : ApiController
    {
        private ICustomerAppService _customerAppService;

        public CustomerServiceController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [Route("GetAll")]
        [Authorize(Roles = "Administrators")]
        [HttpGet]
        public async Task<IOperationResponse> GetAll()
        {
            try
            {
                var ret = await _customerAppService.GetAll();
                return new OperationResponse(true, "", ret);
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }

        [Route("Save")]
        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public async Task<IOperationResponse> Save(CustomerViewModel customer)
        {
            try
            {
                var ret = await _customerAppService.Save(customer);
                if (!ret.ValidationResult.IsValid)
                {
                    return new OperationResponse(false, ret.ValidationResult.ErrorMessages());
                }              

                return new OperationResponse(true, "Saved successfully");                
            }
            catch (Exception ex)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }

        [Route("GetById")]
        [Authorize(Roles = "Administrators")]
        [HttpGet]
        public async Task<IOperationResponse> GetById(Guid id)
        {
            try
            {
                var ret = await _customerAppService.GetById(id);
                return new OperationResponse(true, "", ret);
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }

        [Route("Delete")]
        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public async Task<IOperationResponse> Delete(Guid id)
        {
            try
            {
                await _customerAppService.Remove(id);
                return new OperationResponse(true, "removed successfully");
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }
    }
}
