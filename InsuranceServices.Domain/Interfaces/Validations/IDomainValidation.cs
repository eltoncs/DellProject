using InsuranceServices.Domain.Entities;

namespace InsuranceServices.Domain.Interfaces.Validations
{
    //Named as an interface just for standard purposes
    public abstract class IDomainValidation
    {
        protected Validation _validation = new Validation();
        public Validation Validation { get { return _validation; } }
    }
}
