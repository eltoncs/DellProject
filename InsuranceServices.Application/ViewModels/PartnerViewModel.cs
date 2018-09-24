using InsuranceServices.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InsuranceServices.Application.ViewModels
{
    public class PartnerViewModel
    {
        public PartnerViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(80)]
        [Required]
        public string Name { get; set; }

        public Validation ValidationResult { get; set; }
    }
}
