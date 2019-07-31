using System;
using Infrastructure.Brokers.RabbitMq.Configs;
using Infrastructure.Brokers.RabbitMq.Contracts;
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
            MqChannel.QueueDeclare(queueName, QueueConfig.IsDurable, false, false, null);
        }
        
        public void ClearQueue(string queueName)
        {
            MqChannel.QueuePurge(queueName);
        }
        
        public void DeleteQueue(string queueName)
        {
            MqChannel.QueueDelete(queueName);
        }

        public void SubscribeOnQueue(Action<object, BasicDeliverEventArgs> handler, string queueName)
        {
            var consumer = new EventingBasicConsumer(MqChannel);
            consumer.Received += (model, args) => handler(model, args);

            MqChannel.BasicConsume(queueName, MqChannelConfig.AutoAck, consumer);
        }
    }
}