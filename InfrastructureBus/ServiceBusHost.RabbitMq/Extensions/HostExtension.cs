using CoolBrains.Bus.ServiceBusHost.HostingServices;
using Microsoft.Extensions.DependencyInjection;

namespace CoolBrains.ServiceBusHost.RabbitMq.Extensions
{
	using Infrastructure.SLAdapter.MsDependency;

	public static class HostExtension
    {
        
        public static MassTransitRabbitMqHostingService UseRabbitMq(this CoolServiceBusHostingService coolServiceBusHostingService)
        {
            var massTransitRabbitMqHostingService = CommonServiceLocator.ServiceLocator.Current.GetInstance<MassTransitRabbitMqHostingService>();
            massTransitRabbitMqHostingService.CoolServiceBusHostingService = coolServiceBusHostingService;
            return massTransitRabbitMqHostingService;
        }

        public static CoolServiceBusHostingService Host(this CoolServiceBusHostingService coolBusHostingService, IServiceCollection serviceCollection)
        {
            var adaptor = new MsServiceLocatorAdapter(serviceCollection);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => adaptor);
            return coolBusHostingService;
        }
    }

    
}
