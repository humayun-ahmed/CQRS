using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SuitSupply.Infrastructure.Bus;
using SuitSupply.Infrastructure.Bus.Command;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.Infrastructure.Bus.Query;
using SuitSupply.Infrastructure.SLAdapter.MsDependency;

namespace Bus.Test
{
    public abstract class TestBase
    {
        public ServiceCollection ServiceCollection { get; set; }

        private void Init()
        {
            if (ServiceCollection == null)
            {
                ServiceCollection = new ServiceCollection();
                ServiceCollection.AddSingleton<ISuitBus, SuitInmemoryBus>();
            }
        }

        private void SetServiceLocator()
        {
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => new MsServiceLocatorAdapter(ServiceCollection));
        }
        public void RegisterMockCommandHandler(SuitCommandHandler<TestCommand> commandHandler)
        {
            Init();
            ServiceCollection.AddSingleton<SuitCommandHandler<TestCommand>>(commandHandler);
            SetServiceLocator();
        }

        public void RegisterMockQueryHandler(SuitQueryHandler<string, TestQuery> queryHandler)
        {
            Init();
            ServiceCollection.AddSingleton<SuitQueryHandler<string, TestQuery>>(queryHandler);
            SetServiceLocator();
        }

    }
}
