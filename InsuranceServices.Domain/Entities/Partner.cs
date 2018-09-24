using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceServices.Domain.Entities
{
    public class Partner
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(80)]
        [Required]
        public string Name { get; set; }

        public virtual IList<Statistic> Simulations { get; set; }

        [NotMapped]
        public Validation ValidationResult { get; set; }
    }
}
