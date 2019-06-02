using System.Threading.Tasks;

namespace CoolBrains.Bus.Contracts.Query
{
    public interface IQueryProcessor<TResult, in TQuery> where TQuery: CoolQuery<TResult>
    {
        Task<QueryResponse<TResult>> Process(TQuery query);
    }
}
