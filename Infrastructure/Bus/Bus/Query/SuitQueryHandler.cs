using SuitSupply.Infrastructure.Bus.Contracts.Query;
using SuitSupply.Infrastructure.Validator.Contract;
using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Bus.Query
{
    public abstract class SuitQueryHandler<TResult, TQuery> where TQuery : SuitQuery<TResult>
    {
        public abstract Task<SuitValidationResult> Validate(TQuery query);
        public abstract Task<QueryResponse<TResult>> Handle(TQuery query);

        public async Task<QueryResponse<TResult>> Process(TQuery query)
        {
            var response = new QueryResponse<TResult>();
            var validationResult = await Validate(query);
            if (validationResult.IsValid) return await this.Handle(query);

            response.Success = false;
            response.ValidationResult = validationResult;
            return response;
        }
    }
}
