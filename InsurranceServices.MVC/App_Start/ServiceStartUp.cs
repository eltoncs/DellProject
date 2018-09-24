using InsuranceServices.Application.Interfaces;
using InsuranceServices.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace InsurranceServices.MVC.App_Start
{
    public class ServiceStartUp
    {
        private IInsuranceTypeAppService _insuranceTypeAppService;
        public ServiceStartUp(IInsuranceTypeAppService insuranceTypeAppService)
        {
            _insuranceTypeAppService = insuranceTypeAppService;
        }

        public async Task SaveInsuranceTypes()
        {
            try
            {
                var insuranceType = await _insuranceTypeAppService.GetByName("Car");

                if (insuranceType == null)
                {
                    insuranceType = new InsuranceTypeViewModel()
                    {
                        Name = "Car",
                        BaseIndexValue = (decimal)0.06,
                        UsageYearIndexValue = (decimal)0.05,
                        MaxYearUsage = 20,
                        FixedIndexValue = (decimal)0.15,
                        ExtraForSize = 0
                    };
                    
                };
                await _insuranceTypeAppService.Save(insuranceType);

                insuranceType = await _insuranceTypeAppService.GetByName("Motorcycle");

                if (insuranceType == null)
                {
                    insuranceType = new InsuranceTypeViewModel()
                    {
                        Name = "Motorcycle",
                        BaseIndexValue = (decimal)0.06,
                        UsageYearIndexValue = (decimal)0.05,
                        MaxYearUsage = 20,
                        FixedIndexValue = (decimal)0.25,
                        ExtraForSize = 0
                    };
                };
               
                await _insuranceTypeAppService.Save(insuranceType);

                insuranceType = await _insuranceTypeAppService.GetByName("House");

                if (insuranceType == null)
                {
                    insuranceType = new InsuranceTypeViewModel()
                    {
                        Name = "House",
                        BaseIndexValue = (decimal)0.02,
                        UsageYearIndexValue = 0,
                        MaxYearUsage = 0,
                        FixedIndexValue = 0,
                        ExtraForSize = 0
                    };
                };
               
                await _insuranceTypeAppService.Save(insuranceType);

                insuranceType = await _insuranceTypeAppService.GetByName("Farm");

                if (insuranceType == null)
                {
                    insuranceType = new InsuranceTypeViewModel()
                    {
                        Name = "Farm",
                        BaseIndexValue = (decimal)0.002,
                        UsageYearIndexValue = 0,
                        MaxYearUsage = 0,
                        FixedIndexValue = 0,
                        ExtraForSize = (decimal)0.00001
                    };
                };
               
                await _insuranceTypeAppService.Save(insuranceType);
            }
            catch(Exception ex)
            {
                var r = ex.Message;
            }
            
        }
    }
}