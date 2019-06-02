using CommonServiceLocator;
using SuitSupply.Infrastructure.Bus.Command;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.Infrastructure.Bus.Contracts.Command;
using SuitSupply.Infrastructure.Bus.Contracts.Query;
using SuitSupply.Infrastructure.Bus.Query;
using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Bus
{
    public class SuitInmemoryBus: ISuitBus
    {
        public Task<QueryResponse<TResult>> Query<TResult>(IQuery<TResult> query)
        {
            var queryHandlerType = typeof(SuitQueryHandler<,>).MakeGenericType(typeof(TResult), query.GetType());
            dynamic queryProcessor = ServiceLocator.Current.GetInstance(queryHandlerType);
            return queryProcessor.Process((dynamic)query);
        }

        public Task<CommandResponse> Send(SuitCommand command)
        {
            var commandHandlerType = typeof(SuitCommandHandler<>).MakeGenericType(command.GetType());
            dynamic commandHandler = ServiceLocator.Current.GetInstance(commandHandlerType);
            return commandHandler.Process((dynamic)command);
        }
    }
}
