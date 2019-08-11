using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq.Facades
{
    public interface IMessageFacade
    {
        void PublishMessage(byte[] body, string routingKey, string exchangeName);
        
        void PublishMessageToDefaultExchange(byte[] body, string routingKey);

        void Ack(BasicDeliverEventArgs args, IModel channel);

        void Ack(BasicDeliverEventArgs args);

        void Ack(ulong deliveryTag, IModel channel);

        void Ack(ulong deliveryTag);
        
        byte[] WaitMessageFromQueue(string queueName);
    }
}