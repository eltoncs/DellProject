using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceServices.Domain.Entities
{
    public class Statistic
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        [Required]
        public Guid InsuranceTypeId { get; set; }

        public DateTime CreationDate { get; set; }

        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }

        [ForeignKey("InsuranceTypeId")]
        public virtual InsuranceType InsuranceType { get; set; }

        [NotMapped]
        public Validation ValidationResult { get; set; }
    }
}
