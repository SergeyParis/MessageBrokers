using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Facades.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq.Facades
{
    public class MessageFacade : BaseFacade, IMessageFacade
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
    }
}