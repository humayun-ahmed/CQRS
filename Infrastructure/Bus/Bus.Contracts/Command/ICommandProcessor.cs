using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Bus.Contracts.Command
{
    public interface ICommandProcessor<in T>
    {
        Task<CommandResponse> Process(T command);
    }
}
