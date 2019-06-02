using System.Threading.Tasks;

namespace CoolBrains.Bus.Contracts.Event
{
    public interface IEventProcessor<in T> where T :CoolEvent
    {
        Task Process(T @event);
    }
}
