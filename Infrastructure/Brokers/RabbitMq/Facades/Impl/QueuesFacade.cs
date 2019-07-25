using System;
using Infrastructure.Brokers.RabbitMq.Facades;
using Infrastructure.Brokers.RabbitMq.Interfaces;
using Infrastructure.Brokers.RabbitMq.Interfaces.Impl;
using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq.Factories
{
    public class QueuesFacade : IQueuesFacade
    {
        private QueueConfig _queueConfig;
        private ChannelConfig _channelConfig;
        private IModel _channel;

        public IChannel Channel
        {
            set
            {
                if (value == null || value.MqChannel == null)
                    throw new NullReferenceException();

                _channel = value.MqChannel;
                _channelConfig = value.Config;
            }
            get => new Channel
            {
                Config = _channelConfig,
                MqChannel = _channel
            };
        }

        public QueuesFacade(IChannel channel, QueueConfig queueConfig)
        {
            if (channel == null)
                throw new NullReferenceException();
                
            _channelConfig = channel.Config;
            _channel = channel.MqChannel;
            _queueConfig = queueConfig;
        }
        
        public void CreateQueue(string queueName)
        {
            _channel.QueueDeclare(queueName, _queueConfig.IsDurable, false, false, null);
        }
        
        public void ClearQueue(string queueName)
        {
            _channel.QueuePurge(queueName);
        }
        
        public void DeleteQueue(string queueName)
        {
            _channel.QueueDelete(queueName);
        }

        public void SubscribeOnQueue(Action<object, BasicDeliverEventArgs> handler, string queueName)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, args) => handler(model, args);

            _channel.BasicConsume(queueName, _channelConfig.AutoAck, consumer);
        }
    }
}