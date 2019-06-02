using System;

namespace CoolBrains.Bus.Contracts
{
    public abstract class CoolMessage
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
