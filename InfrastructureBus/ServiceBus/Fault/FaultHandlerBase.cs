using System.Linq;
using CoolBrains.Bus.Contracts.Fault;

namespace CoolBrains.Bus.ServiceBus.Fault
{
    public class FaultHandlerBase
    {
        public FaultInfo BuildFaultInfo(MassTransit.Fault fault)
        {
            var exception = fault.Exceptions.FirstOrDefault();
            if (exception == null)
                return new FaultInfo();
            return new FaultInfo
            {
                Message = exception.Message,
                ExceptionType = exception.ExceptionType,
                Source = exception.Source,
                StackTrace = exception.StackTrace
            };
        }

        
    }
}
