namespace Infrastructure.SLAdapter.MsDependency
{
	using System;
	using System.Collections.Generic;

	using Microsoft.Extensions.DependencyInjection;

	public class MsServiceLocatorAdapter : CommonServiceLocator.IServiceLocator
    {
        public IServiceProvider ServiceProvider { get; set; }
        public MsServiceLocatorAdapter(IServiceCollection serviceCollection)
        {
            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public MsServiceLocatorAdapter(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            var instance = this.ServiceProvider.GetService(serviceType);
            if (instance == null)
                throw new Exception($"No registration has found for type {serviceType.ToString()}, Please add registration for it.");
            return instance;
        }

        public object GetInstance(Type serviceType)
        {
            return this.GetService(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return this.GetService(serviceType);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this.ServiceProvider.GetServices(serviceType);
        }

        public TService GetInstance<TService>()
        {
            var instance = this.ServiceProvider.GetService<TService>();
            if (instance == null)
                throw new Exception($"No registration has found for type {typeof(TService).ToString()}, Please add registration for it.");
            return instance;
        }

        public TService GetInstance<TService>(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return this.ServiceProvider.GetServices<TService>();
        }
    }
}
