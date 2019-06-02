namespace CoolBrains.Bus.Contracts.Query
{
    public abstract class CoolQuery<TResult> : IQuery<TResult>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public interface IQuery<TResult>
    {
    }
}
