using CommonServiceLocator;
using CoolBrains.Bus.Contracts;
using CoolBrains.Bus.Contracts.Command;
using CoolBrains.Bus.Contracts.Event;
using CoolBrains.Bus.Contracts.Query;
using System.Threading.Tasks;

namespace CoolBrains.Bus.ServiceBus.Bus
{
    public class CoolInmemoryBus : IInmemoryBus
    {
        public Task<QueryResponse<TResult>> Query<TResult>(IQuery<TResult> query)
        {
            var queryProcessorType = typeof(IQueryProcessor<,>).MakeGenericType(typeof(TResult), query.GetType());
            dynamic queryProcessor =  ServiceLocator.Current.GetInstance(queryProcessorType);
            return queryProcessor.Process((dynamic) query);
        }

        public Task<CommandResponse> Send(CoolCommand command)
        {
            var commandProcessorType = typeof(ICommandProcessor<>).MakeGenericType(command.GetType());
            dynamic commandProcessor = ServiceLocator.Current.GetInstance(commandProcessorType);
            return commandProcessor.Process((dynamic)command);
        }

        public Task Publish(CoolEvent @event)
        {
            var eventProcessorType = typeof(IEventProcessor<>).MakeGenericType(@event.GetType());
            dynamic eventProcessor = ServiceLocator.Current.GetInstance(eventProcessorType);
            return eventProcessor.Process((dynamic)@event);
        }
    }
}