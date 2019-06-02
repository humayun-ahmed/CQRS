using System.Threading.Tasks;

namespace CoolBrains.Bus.Contracts.Command
{
    public interface ICommandProcessor<in T>
    {
        Task<CommandResponse> Process(T command);
    }
}
