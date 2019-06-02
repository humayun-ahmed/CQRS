using System;
using System.Collections.Generic;
using System.Text;
using SuitSupply.Infrastructure.Bus.Contracts.Command;

namespace Bus.Test
{
    public class TestCommand : SuitCommand
    {
        public string UserName { get; set; }

    }
}
