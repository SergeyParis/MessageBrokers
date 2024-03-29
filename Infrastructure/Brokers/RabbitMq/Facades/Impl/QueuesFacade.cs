using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Contracts.Configs;
using Infrastructure.Brokers.RabbitMq.Providers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq.Facades.Impl
{
    internal class QueuesFacade : BaseFacade, IQueuesFacade
    {
        public QueueConfig QueueConfig { get; set; }

        public QueuesFacade(IChannelProvider channelProvider, QueueConfig queueConfig)
            : base(channelProvider)
        {
            QueueConfig = queueConfig;
        }
        
        public void CreateQueue(string queueName)
        {
            MqChannel.QueueDeclare(queueName, QueueConfig.Durable, false, false, null);
        }
        
        public void ClearQueue(string queueName)
        {
            MqChannel.QueuePurge(queueName);
        }
        
        public void DeleteQueue(string queueName)
        {
            MqChannel.QueueDelete(queueName);
        }

        public void SubscribeOnQueue(Action<IModel, BasicDeliverEventArgs> handler, string queueName)
        {
            var consumer = new EventingBasicConsumer(MqChannel);
            consumer.Received += (channel, args) => handler(MqChannel, args);

            MqChannel.BasicConsume(queueName, MqChannelConfig.AutoAck, consumer);
        }
        
        public void BindQueue(string queueName, string exchangeName, string routingKey)
        {
            MqChannel.QueueBind(queueName, exchangeName, routingKey, null);
        }
    }
}