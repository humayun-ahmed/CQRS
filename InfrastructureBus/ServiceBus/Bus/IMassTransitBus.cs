using System.Threading.Tasks;
using MassTransit;

namespace CoolBrains.Bus.ServiceBus.Bus
{
    public interface IMassTransitBus
    {
        IBusControl BuildBus();
        Task Send<T>(T command, string queueName = "") where T:class;
        Task Publish<T>(T @event) where T : class;
    }
}
