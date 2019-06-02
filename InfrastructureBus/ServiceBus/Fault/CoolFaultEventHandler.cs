using System.Threading.Tasks;
using CoolBrains.Bus.Contracts.Event;
using CoolBrains.Bus.Contracts.Fault;
using MassTransit;

namespace CoolBrains.Bus.ServiceBus.Fault
{
    public abstract class CoolFaultEventHandler<T> : FaultHandlerBase, IFaultProcessor<T>, IConsumer<Fault<T>> where T : CoolEvent
    {
        public abstract Task Handle(T @event, FaultInfo fault);

        public Task Consume(ConsumeContext<Fault<T>> context)
        {
            var faultInfo = BuildFaultInfo(context.Message);
            return Process(context.Message.Message, faultInfo);
        }

        public Task Process(T message, FaultInfo fault)
        {
            return Handle(message, fault);
        }
    }
}


