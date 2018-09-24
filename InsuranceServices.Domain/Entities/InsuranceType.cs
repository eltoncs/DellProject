using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceServices.Domain.Entities
{
    public class InsuranceType
    {
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

        [NotMapped]
        public Validation ValidationResult { get; set; }
    }
}
