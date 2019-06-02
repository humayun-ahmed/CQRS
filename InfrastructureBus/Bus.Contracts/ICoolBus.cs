using System.Threading.Tasks;
using CoolBrains.Bus.Contracts.Command;
using CoolBrains.Bus.Contracts.Event;
using CoolBrains.Bus.Contracts.Query;

namespace CoolBrains.Bus.Contracts
{
    public interface ICoolBus: IInmemoryBus,IExternalBus
    {
       

    }

    public interface IExternalBus
    {
        Task SendUsingMedia<T>(T command, string queueName = "") where T : CoolCommand;
        Task PublishUsingMedia<T>(T @event) where T : CoolEvent;

    }
    public interface IInmemoryBus
    {
        Task<QueryResponse<TResult>> Query<TResult>(IQuery<TResult> query);
        Task<CommandResponse> Send(CoolCommand command);
        Task Publish(CoolEvent @event);

    }
}
