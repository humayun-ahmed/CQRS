using System;


namespace CoolBrains.Bus.ServiceBusHost.HostingServices
{
	using Infrastructure.SLAdapter.MsDependency;

	public class CoolServiceBusHostingService
    {
        public CoolServiceBusHostingService Host(IServiceProvider serviceProvider)
        {
            var adapter = new MsServiceLocatorAdapter(serviceProvider);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => adapter);
            return this;
        }
    }
}
