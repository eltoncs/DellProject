using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceServices.Domain.Entities
{
    public class CustomerInsurance
    {
        [Key]
        public Guid Id { get; set; }

        public Guid InsuranceTypeId { get; set; }
        public Guid CustomerId { get; set; }

        [ForeignKey("InsuranceTypeId")]
        public virtual InsuranceType InsuranceType { get;set;}

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
