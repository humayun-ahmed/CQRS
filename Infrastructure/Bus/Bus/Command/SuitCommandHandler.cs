using SuitSupply.Infrastructure.Bus.Contracts.Command;
using SuitSupply.Infrastructure.Validator.Contract;
using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Bus.Command
{
    public abstract class SuitCommandHandler<TCommand> where TCommand : SuitCommand
    {
        public abstract Task<SuitValidationResult> Validate(TCommand command);
        public abstract Task<CommandResponse> Handle(TCommand command);
        public async Task<CommandResponse> Process(TCommand command)
        {
            var validationResult = await Validate(command);
            if (validationResult.IsValid)
            {
                return await Handle(command);
            }
            return new CommandResponse { ValidationResult = validationResult, Success = false };
        }
    }
}
