namespace CoolBrains.Bus.ServiceBusHost.HostingServices
{
    public class ServiceBusHostProvider
    {
        public static CoolServiceBusHostingService Get()
        {
            return new CoolServiceBusHostingService();
        }
    }
}
