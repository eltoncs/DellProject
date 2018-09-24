using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Interfaces
{
    public interface IOperationResponse
    {
        bool Success { get; }
        string Message { get; }
        dynamic Data { get; }
    }
}
