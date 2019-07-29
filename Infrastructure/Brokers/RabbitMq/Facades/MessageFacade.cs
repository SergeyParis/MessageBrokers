using System.Threading;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Facades.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq.Facades
{
    internal class MessageFacade : BaseFacade, IMessageFacade
    {
        public MessageFacade(IChannelProvider channelProvider) : base(channelProvider)
        {
        }

        public void PublishMessage(byte[] body, string routingKey)
        {
            MqChannel.BasicPublish("", routingKey, null, body);
        }

        public void Ack(BasicDeliverEventArgs args)
        {
            MqChannel.BasicAck(args.DeliveryTag, false);
        }
        
        public byte[] WaitMessageFromQueue(string queueName)
        {
            BasicGetResult result = null;
            while (result == null)
            {
                result = MqChannel.BasicGet(queueName, MqChannelConfig.AutoAck);
                Thread.Sleep(50);
            }

            return result.Body;
        }
    }
}