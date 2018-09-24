using System.ComponentModel.DataAnnotations;

namespace InsuranceServices.Application.ViewModels
{
    public class SummaryStatisticViewModel
    {
        [Key]
        public string InsuranceType { get; set; }
        public int Quantity { get; set; }
    }
}
