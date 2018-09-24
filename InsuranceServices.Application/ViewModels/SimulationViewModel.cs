using System;

namespace InsuranceServices.Application.ViewModels
{
    public class SimulationViewModel
    {
        public Guid PartnerId { get; set; }
        public String PropertyType { get; set; }
        public decimal Value { get; set; }
        public int ManufacacturedIn { get; set; }
        public decimal Area { get; set; }
    }
}
