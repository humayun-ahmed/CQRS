using System.Threading.Tasks;

namespace CoolBrains.Bus.Contracts.Fault
{
    public interface IFaultProcessor<in T>
    {
        Task Process(T message, FaultInfo fault);
    }

}
