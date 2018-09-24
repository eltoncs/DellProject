using System.Collections.Generic;
using System.Text;

namespace InsuranceServices.Domain.Entities
{
    public class Validation
    {
        private List<string> _brokenRules = new List<string>();

        public Validation()
        {
            _brokenRules = new List<string>();
        }

        public Validation(List<string> brokenRules = null)
        {
            _brokenRules = brokenRules;
        }

        public bool IsValid {
            get { return _brokenRules.Count == 0; }
        }

        public List<string> BrokenRules {
            get { return _brokenRules; }
            set { _brokenRules = value; }
        }

        public string ErrorMessages()
        {
            var sb = new StringBuilder();

            foreach(var item in _brokenRules)
            {
                sb.Append(item+";");
            }

            return sb.ToString();
        }
    }
}
