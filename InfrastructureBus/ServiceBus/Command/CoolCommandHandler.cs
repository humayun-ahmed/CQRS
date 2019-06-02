using CoolBrains.Bus.Contracts.Command;
using System.Threading.Tasks;
using MassTransit;

namespace CoolBrains.Bus.ServiceBus.Command
{
    public abstract class CoolCommandHandler<TCommand> : ICommandProcessor<TCommand>, IConsumer<TCommand> where TCommand : CoolCommand
    {
        
        public abstract CommandResponse Handle(TCommand command);
        public Task<CommandResponse> Process(TCommand command)
        {
            var  commandResponse = Handle(command);
            return Task.FromResult(commandResponse);
        }

        public Task Consume(ConsumeContext<TCommand> context)
        {
             return Process(context.Message);
        }
    }
}
