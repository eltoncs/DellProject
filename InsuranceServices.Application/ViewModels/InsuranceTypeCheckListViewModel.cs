using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceServices.Application.ViewModels
{
    public class InsuranceTypeCheckListViewModel
    {
        public InsuranceTypeCheckListViewModel()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public Guid InsuranceTypeId { get; set; }
    }
}
