using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Bus.Contracts.Fault
{
    public class FaultInfo
    {
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
    }
}
