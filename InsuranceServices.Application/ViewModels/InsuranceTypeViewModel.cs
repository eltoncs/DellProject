using InsuranceServices.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InsuranceServices.Application.ViewModels
{
    public class InsuranceTypeViewModel
    {
        public InsuranceTypeViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal BaseIndexValue { get; set; }

        [Required]
        public decimal UsageYearIndexValue { get; set; }

        [Required]
        public decimal FixedIndexValue { get; set; }

        [Required]
        public int MaxYearUsage { get; set; }

        [Required]
        public decimal ExtraForSize { get; set; }

        public Validation ValidationResult { get; set; }
    }
}
