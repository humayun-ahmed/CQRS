using System;
using System.Threading.Tasks;
using CoolBrains.Bus.Contracts;
using CoolBrains.Bus.Contracts.Command;
using CoolBrains.Bus.Contracts.Event;

namespace CoolBrains.Bus.ServiceBus.Bus
{
    public class CoolMassTransitBus : IExternalBus
    {
        public IMassTransitBus MassTransitBus { get; set; }
        public CoolMassTransitBus(IMassTransitBus massTransitBus)
        {

            MassTransitBus = massTransitBus;
        }

        public async Task SendUsingMedia<T>(T command, string queueName = "") where T : CoolCommand
        {
            //SetAuthorizationDataToMessage(command);
            await MassTransitBus.Send(command, queueName);

        }

        public async Task PublishUsingMedia<T>(T @event) where T : CoolEvent
        {
            //SetAuthorizationDataToMessage(@event);
            await MassTransitBus.Publish(@event);
        }
    }
}