

using SuitSupply.Infrastructure.Validator.Contract;

namespace SuitSupply.Infrastructure.Bus.Contracts.Query
{
    public class QueryResponse<TResult>
    {
        public bool Success { get; set; }
        public TResult Data { get; set; }
        public int TotalCount { get; set; }
        public SuitValidationResult ValidationResult { get; set; }
        public string ErrorMessage { get; set; }
    }
}
