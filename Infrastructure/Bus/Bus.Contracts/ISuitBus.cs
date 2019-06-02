using System.Threading.Tasks;
using SuitSupply.Infrastructure.Bus.Contracts.Command;
using SuitSupply.Infrastructure.Bus.Contracts.Query;

namespace SuitSupply.Infrastructure.Bus.Contracts
{
    public interface ISuitBus
    {
        Task<QueryResponse<TResult>> Query<TResult>(IQuery<TResult> query);
        Task<CommandResponse> Send(SuitCommand command);
    }
}
