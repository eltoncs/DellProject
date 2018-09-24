using InsuranceServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceServices.Application.ViewModels
{
    public class CustomerInsuranceViewModel
    {
        public CustomerInsuranceViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid InsuranceTypeId { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public InsuranceType Customer { get; set; }
    }
}
