using InsuranceServices.Application.Interfaces;
using InsuranceServices.Application.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace InsurranceServices.MVC.Controllers
{
    [RoutePrefix("Api/PartnerService")]
    public class PartnerServiceController : ApiController
    {
        private IPartnerAppService _partnerAppService;

        public PartnerServiceController(IPartnerAppService partnerAppService)
        {
            _partnerAppService = partnerAppService;
        }

        [Route("Save")]
        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public async Task<IOperationResponse> Save(PartnerViewModel partner)
        {
            try
            {
                var ret = await _partnerAppService.Save(partner);
                if (ret.ValidationResult.IsValid)
                {
                    return new OperationResponse(true, "Saved successfully");
                }

                return new OperationResponse(false, ret.ValidationResult.ErrorMessages());
            }
            catch(Exception)
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
                await _partnerAppService.Remove(id);
                return new OperationResponse(true, "removed successfully");
            }
            catch (Exception ex)
            {
                return new OperationResponse(false, ex.Message);
            }
        }

        [Route("GetAll")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IOperationResponse> GetAll()
        {
            try
            {
                var ret = await _partnerAppService.GetAll();
                return new OperationResponse(true, "", ret);
            }
            catch (Exception)
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
                var ret = await _partnerAppService.GetById(id);
                return new OperationResponse(true, "", ret);
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }
    }
}
