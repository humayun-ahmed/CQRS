using System;
using System.Collections.Generic;
using CoolBrains.Bus.ServiceBusHost.HostingServices;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Options;

namespace CoolBrains.ServiceBusHost.RabbitMq
{
    public class MassTransitRabbitMqHostingService : IExternalBusService
    {
        public MassTransitRabbitMqHostingService()
        {
            QueueConfigurations = new Dictionary<string, Action<IRabbitMqReceiveEndpointConfigurator>>();
        }

        private Dictionary<string, Action<IRabbitMqReceiveEndpointConfigurator>> QueueConfigurations { get; set; }
        public CoolServiceBusHostingService CoolServiceBusHostingService { get; set; }
        public IBusControl BusControl { get; set; }
        public RabbitConfig RabbitConfig { get; set; }
        public IRabbitMqHost RabbitMqHost { get; set; }
        public IRabbitMqBusFactoryConfigurator RabbitMqBusFactoryConfigurator { get; set; }
        private int RetryCount { get; set; } = 0;
        private int RetryIntervalInSec { get; set; } = 0;

        public void InitializeBusUsingRabbitMq()
        {
            RabbitConfig = CommonServiceLocator.ServiceLocator.Current.GetInstance<IOptions<RabbitConfig>>().Value;
            BusControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                RabbitMqHost = cfg.Host(new Uri($"rabbitmq://{RabbitConfig.HostName}:{RabbitConfig.Port}/"), h =>
                {
                    h.Username(RabbitConfig.UserName);
                    h.Password(RabbitConfig.Password);
                });
                this.RabbitMqBusFactoryConfigurator = cfg;


                foreach (var queueConfiguration in QueueConfigurations)
                {
                    cfg.ReceiveEndpoint(RabbitMqHost, queueConfiguration.Key, queueConfiguration.Value);
                }


                if (RetryCount > 0)
                {
                    cfg.UseRetry(config =>
                    {
                        config.Interval(RetryCount, TimeSpan.FromSeconds(RetryIntervalInSec));
                    });
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName">Define Your QueueName, By default "_CoolBus" suffix will be added with your queue name</param>
        /// <param name="queueConfiguration">You can define queue configuration from here like Prefetch Count,Concurrent Thread Limit you can check following example.
        /// e =>
        ///{
        ///     e.LoadFrom(container);
        ///     e.PrefetchCount = 1;
        ///     e.UseConcurrencyLimit(1);
        ///}
        /// NOTE: You must write e.LoadFrom(container); if you use queueConfiguration
        /// </param>
        /// <returns></returns>
        public MassTransitRabbitMqHostingService ListenOn(string queueName, Action<IRabbitMqReceiveEndpointConfigurator> queueConfiguration = null)
        {
            /*
            if (queueConfiguration == null)
                queueConfiguration = configurator =>
                {
                    configurator.LoadFrom(Container);
                };
            */
            QueueConfigurations.Add(queueName + "_CoolBus", queueConfiguration);
            return this;
        }

        public MassTransitRabbitMqHostingService UseRetry(int count = 3, int intervalInSec = 2)
        {
            RetryCount = count;
            RetryIntervalInSec = intervalInSec;
            return this;
        }
        public void Start()
        {
            InitializeBusUsingRabbitMq();
            BusControl.Start();
        }

        public MassTransitRabbitMqHostingService ListenCommandsOn(string queueName, Action<IRabbitMqReceiveEndpointConfigurator> configure)
        {
            QueueConfigurations.Add(queueName, configure);
            return this;
        }
        public MassTransitRabbitMqHostingService ListenEventsOn(string queueName, Action<IRabbitMqReceiveEndpointConfigurator> configure)
        {
            QueueConfigurations.Add(queueName, configure);
            return this;
        }

    }
}
