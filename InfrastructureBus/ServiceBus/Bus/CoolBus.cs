using System.Threading.Tasks;
using CoolBrains.Bus.Contracts;
using CoolBrains.Bus.Contracts.Command;
using CoolBrains.Bus.Contracts.Event;
using CoolBrains.Bus.Contracts.Query;

namespace CoolBrains.Bus.ServiceBus.Bus
{
    public class CoolBus : ICoolBus
    {
        public IExternalBus ExternalBus { get; set; }
        public IInmemoryBus InmemoryBus { get; set; }

        public CoolBus(IInmemoryBus inmemoryBus)
        {
            InmemoryBus = inmemoryBus;
        }

        public Task SendUsingMedia<T>(T command, string destination) where T : CoolCommand
        {
            ExternalBus = ExternalBus ?? CommonServiceLocator.ServiceLocator.Current.GetInstance<IExternalBus>();
            return ExternalBus.SendUsingMedia<T>(command, destination);
        }

        public Task PublishUsingMedia<T>(T @event) where T : CoolEvent
        {
            ExternalBus = ExternalBus ?? CommonServiceLocator.ServiceLocator.Current.GetInstance<IExternalBus>();
            return ExternalBus.PublishUsingMedia<T>(@event);
        }

        public Task<QueryResponse<TResult>> Query<TResult>(IQuery<TResult> query)
        {
            return InmemoryBus.Query<TResult>(query);
        }

        public Task<CommandResponse> Send(CoolCommand command)
        {
            return InmemoryBus.Send(command);
        }

        public Task Publish(CoolEvent @event)
        {
            return InmemoryBus.Publish(@event);
        }
    }
}
