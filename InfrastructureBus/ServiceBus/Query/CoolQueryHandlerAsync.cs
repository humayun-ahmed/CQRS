using System.Threading.Tasks;
using CoolBrains.Bus.Contracts.Query;

namespace CoolBrains.Bus.ServiceBus.Query
{
    public abstract class CoolQueryHandlerAsync<TResult,TQuery> : IQueryProcessor<TResult,TQuery> where TQuery : CoolQuery<TResult>
    {
        public abstract Task<QueryResponse<TResult>> Handle(TQuery query);
        public async Task<QueryResponse<TResult>> Process(TQuery query)
        {
            return await this.Handle(query);
        }

      
    }


}
