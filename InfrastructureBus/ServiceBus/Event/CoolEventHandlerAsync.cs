using System.Threading.Tasks;
using CoolBrains.Bus.Contracts.Event;
using MassTransit;

namespace CoolBrains.Bus.ServiceBus.Event
{
    public abstract class CoolEventHandlerAsync<T> : IEventProcessor<T>, IConsumer<T> where T : CoolEvent
    {
        public abstract Task Handle(T @event);
        public Task Consume(ConsumeContext<T> context)
        {
            return Process(context.Message);
        }

        public Task Process(T @event)
        {
            return Handle(@event);
        }
    }
}
