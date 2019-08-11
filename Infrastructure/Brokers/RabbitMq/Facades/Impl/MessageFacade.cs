using System.Threading;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Providers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq.Facades.Impl
{
    internal class MessageFacade : BaseFacade, IMessageFacade
    {
        public MessageFacade(IChannelProvider channelProvider) : base(channelProvider)
        {
        }

        public void PublishMessage(byte[] body, string routingKey, string exchangeName)
        {
            MqChannel.BasicPublish(exchangeName, routingKey, null, body);
        }

        public void PublishMessageToDefaultExchange(byte[] body, string routingKey) =>
            PublishMessage(body, routingKey, string.Empty);

        public void Ack(BasicDeliverEventArgs args, IModel channel)
        {
            channel.BasicAck(args.DeliveryTag, false);
        }

        public void Ack(BasicDeliverEventArgs args) => Ack(args, MqChannel);

        public void Ack(ulong deliveryTag, IModel channel) =>
            Ack(new BasicDeliverEventArgs {DeliveryTag = deliveryTag}, channel);
        
        public void Ack(ulong deliveryTag) => Ack(deliveryTag, MqChannel);

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