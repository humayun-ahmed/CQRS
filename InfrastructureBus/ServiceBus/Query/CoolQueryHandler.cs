using System.Threading.Tasks;
using CoolBrains.Bus.Contracts.Query;

namespace CoolBrains.Bus.ServiceBus.Query
{
    public abstract class CoolQueryHandler<TResult, TQuery> : IQueryProcessor<TResult, TQuery> where TQuery : CoolQuery<TResult>
    {
        public abstract QueryResponse<TResult> Handle(TQuery query);

        public Task<QueryResponse<TResult>> Process(TQuery query)
        {
            var response = this.Handle(query);
            return Task.FromResult(response);
        }
    }


}
