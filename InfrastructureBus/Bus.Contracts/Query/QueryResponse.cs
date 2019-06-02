

namespace CoolBrains.Bus.Contracts.Query
{
    public class QueryResponse<TResult>
    {
        public bool Success { get; set; }
        public TResult Data { get; set; }
        public int TotalCount { get; set; }
        public object ValidationResult { get; set; } //TODO it should be specific type
        public string ErrorMessage { get; set; }
    }
}
