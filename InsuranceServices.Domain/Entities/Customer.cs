using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceServices.Domain.Entities
{
    public class Customer
    {
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

        public virtual ICollection<CustomerInsurance> Insurances { get; set; }
        public virtual ICollection<Statistic> Statistics { get; set; }

        [NotMapped]
        public Validation ValidationResult { get; set; }
    }
}
