using InsuranceServices.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceServices.Application.ViewModels
{
    public class OperationResponse : IOperationResponse
    {
        private bool _success;
        private string _message;
        private dynamic _data;

        public OperationResponse(bool success, string message, dynamic data = null)
        {
            _success = success;
            _message = message;
            _data = data;
        }

        public string Message
        {
            get
            {
                return _message;
            }          
        }

        public bool Success
        {
            get
            {
                return _success;
            }         
        }

        public dynamic Data
        {
            get
            {
                return _data;
            }
        }
    }
}
