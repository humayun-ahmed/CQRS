using System;
using System.Threading.Tasks;
using CoolBrains.Bus.ServiceBus.Bus;
using MassTransit;
using Microsoft.Extensions.Options;

namespace CoolBrains.ServiceBusHost.RabbitMq
{
    public class RabbitMqMassTransitBus : IMassTransitBus
    {

        public IBusControl BusControl { get; set; }
        
        public RabbitConfig RabbitConfig { get; set; }

        public RabbitMqMassTransitBus(IOptions<RabbitConfig> rabbitConfig)
        {
            RabbitConfig = rabbitConfig.Value;
        }

        public IBusControl BuildBus()
        {
            return MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri($"rabbitmq://{this.RabbitConfig.HostName}:{this.RabbitConfig.Port}"), h =>
                {
                    h.Username(this.RabbitConfig.UserName);
                    h.Password(this.RabbitConfig.Password);
                });
            });

        }

        public async Task Send<T>(T command, string queueName = "") where T : class
        {

            BusControl = BusControl ?? BuildBus();
            if (queueName == string.Empty)
            {
                queueName = command.GetType().Assembly.GetName().Name + "_CoolBus";
            }

            var sendToUri = new Uri($"rabbitmq://{RabbitConfig.HostName}:{RabbitConfig.Port}/{queueName}");
            var endPoint = await BusControl.GetSendEndpoint(sendToUri);
            await endPoint.Send(command);

        }

        public async Task Publish<T>(T @event) where T : class
        {
            BusControl = BusControl ?? BuildBus();
            await BusControl.Publish(@event);
        }
    }
}
