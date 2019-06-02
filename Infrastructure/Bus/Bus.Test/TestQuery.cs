using System;
using System.Collections.Generic;
using System.Text;
using SuitSupply.Infrastructure.Bus.Contracts.Query;

namespace Bus.Test
{
    public class TestQuery:SuitQuery<string>
    {
        public string Name { get; set; }

    }
}
