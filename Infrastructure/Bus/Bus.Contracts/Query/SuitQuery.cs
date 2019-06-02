namespace SuitSupply.Infrastructure.Bus.Contracts.Query
{
    public abstract class SuitQuery<TResult> : IQuery<TResult>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public interface IQuery<TResult>
    {
    }
}
