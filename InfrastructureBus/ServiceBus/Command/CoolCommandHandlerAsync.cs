using System.Threading.Tasks;
using CoolBrains.Bus.Contracts.Command;
using MassTransit;

namespace CoolBrains.Bus.ServiceBus.Command
{
    public abstract class CoolCommandHandlerAsync<TCommand> : ICommandProcessor<TCommand>, IConsumer<TCommand> where TCommand : CoolCommand
    {
        public abstract Task<CommandResponse> Handle(TCommand command);
        public Task<CommandResponse> Process(TCommand command)
        {
            return Handle(command);
        }

        public Task Consume(ConsumeContext<TCommand> context)
        {
            return Process(context.Message);
        }
    }


}
