using System.Threading.Tasks;
using CoolBrains.Bus.Contracts.Command;
using CoolBrains.Bus.Contracts.Fault;
using MassTransit;

namespace CoolBrains.Bus.ServiceBus.Fault
{
    public abstract class CoolFaultCommandHandler<T> : FaultHandlerBase, IFaultProcessor<T>, IConsumer<Fault<T>> where T : CoolCommand
    {
        public abstract Task Handle(T command, FaultInfo fault);


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
