using InsuranceServices.Application.Interfaces;
using InsuranceServices.Application.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace InsurranceServices.MVC.Controllers.WebApi
{
    [RoutePrefix("Api/InsuranceTypeService")]
    public class InsuranceTypeServiceController : ApiController
    {
        private IInsuranceTypeAppService _InsuranceTypeAppService;
        public InsuranceTypeServiceController(IInsuranceTypeAppService InsuranceTypeAppService)
        {
            _InsuranceTypeAppService = InsuranceTypeAppService;
        }

        [Route("GetAll")]
        [AllowAnonymous]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<IOperationResponse> GetAll()
        {
            try
            {
                var ret = await _InsuranceTypeAppService.GetAll();
                return new OperationResponse(true, "Ok", ret);
            }
            catch (Exception ex)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }

        [Route("GetCheckList")]
        [Authorize(Roles = "Administrators")]
        [HttpGet]
        public async Task<IOperationResponse> GetCheckList()
        {
            try
            {
                var ret = await _InsuranceTypeAppService.GetCheckList();
                return new OperationResponse(true, "Ok", ret);
            }
            catch (Exception)
            {
                return new OperationResponse(false, "Could not execute the operation");
            }
        }        
    }
}
