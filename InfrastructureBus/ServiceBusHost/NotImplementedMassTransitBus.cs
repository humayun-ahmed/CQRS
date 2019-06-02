using System;
using System.Threading.Tasks;
using CoolBrains.Bus.ServiceBus.Bus;
using MassTransit;

namespace CoolBrains.Bus.ServiceBusHost
{
    public class NotImplementedMassTransitBus: IMassTransitBus
    {
        public IBusControl BuildBus()
        {
            throw new NotImplementedException();
        }

        public Task Send<T>(T command, string queueName = "") where T : class
        {
            throw new NotImplementedException();
        }

        public Task Publish<T>(T @event) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
