using InsuranceServices.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InsuranceServices.Application.ViewModels
{
    public class StatisticViewModel
    {
        public StatisticViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        [Required]
        public Guid InsuranceTypeId { get; set; }

        public DateTime CreationDate { get; set; }

        public Validation ValidationResult { get; set; }

        //public Partner Partner { get; set; }

        //public InsuranceType InsuranceType { get; set; }
    }
}
