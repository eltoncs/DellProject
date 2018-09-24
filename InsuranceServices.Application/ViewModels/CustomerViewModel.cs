using InsuranceServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceServices.Application.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(80)]
        [Required]
        public string Name { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [MaxLength(60)]
        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        [MaxLength(120)]
        public string Address { get; set; }

        public List<InsuranceTypeCheckListViewModel> Insurances { get; set; }

        public Validation ValidationResult { get; set; }
    }
}
