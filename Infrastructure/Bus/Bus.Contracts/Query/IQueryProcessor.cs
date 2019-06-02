using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Bus.Contracts.Query
{
    public interface IQueryProcessor<TResult, in TQuery> where TQuery: SuitQuery<TResult>
    {
        Task<QueryResponse<TResult>> Process(TQuery query);
    }
}
