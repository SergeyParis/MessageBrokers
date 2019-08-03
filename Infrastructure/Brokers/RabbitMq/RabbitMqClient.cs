using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Infrastructure.Brokers.RabbitMq.Contracts.Configs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq
{
    [Obsolete]
    public class RabbitMqClient : IDisposable
    {
        private const string LocalHostName = "localhost";
        
        private readonly IConnection _connection;
        private readonly Dictionary<string, ChannelInfo> _channels;

        private ChannelConfig _channelConfig; 
        private QueueConfig _defaultQueueConfig;

        public RabbitMqClient(string host = LocalHostName, ChannelConfig? channelConfig = null)
        {
            _channels = new Dictionary<string, ChannelInfo>();
            InitConfig(channelConfig);
            
            var factory = new ConnectionFactory {HostName = host};
            _connection = factory.CreateConnection();
        }

        public void PublishMessage(byte[] body, string routingKey)
        {
            GetChannelInfo().Channel.BasicPublish("", routingKey, null, body);
        }

        public void SubscribeOnQueue(Action<object, BasicDeliverEventArgs> handler, string queueName)
        {
            var channelInfo = GetChannelInfo();
            var channel = channelInfo.Channel;
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, args) => handler(model, args);

            channel.BasicConsume(queueName, channelInfo.Config.AutoAck, consumer);
        }

        public void Ack(BasicDeliverEventArgs args)
        {
            var channel = GetChannelInfo().Channel;
            channel.BasicAck(args.DeliveryTag, false);
        }

        public byte[] WaitMessageFromQueue(string queueName)
        {
            var channelInfo = GetChannelInfo();

            BasicGetResult result = null;
            while (result == null)
            {
                result = channelInfo.Channel.BasicGet(queueName, channelInfo.Config.AutoAck);
                Thread.Sleep(50);
            }

            return result.Body;
        }

        public void CreateQueue(string queueName, QueueConfig? queueConfig = null)
        {
            var channel = GetChannelInfo().Channel;
            var config = queueConfig ?? _defaultQueueConfig;
            channel.QueueDeclare(queueName, config.Durable, false, false, null);
        }

        public void ClearQueue(string queueName)
        {
            GetChannelInfo().Channel.QueuePurge(queueName);
        }

        public void DeleteQueue(string queueName)
        {
            GetChannelInfo().Channel.QueueDelete(queueName);
        }
        
        public void Dispose()
        {
            _connection?.Dispose();
            foreach (var channel in _channels)
                channel.Value.Channel.Dispose();
        }
        
        private ChannelInfo GetChannelInfo()
        {
            // channel - program connection to Rabbit MQ (new channel must create for new threads)
            // auto-create new channel for every new thread
            var channelName = Thread.CurrentThread.ManagedThreadId.ToString();
            ChannelInfo channel;

            if (_channels.Any(x => x.Key == channelName))
                channel = _channels[channelName];
            else
            {
                channel = new ChannelInfo
                {
                    Channel = _connection.CreateModel(),
                    Config = _channelConfig
                };
                
                _channels.Add(channelName, channel);
            }
            
            ConfigureChannel(channel);
            return channel;
        }

        private void InitConfig(ChannelConfig? channelConfig)
        {
            _channelConfig = channelConfig ?? new ChannelConfig
            {
                AutoAck = false,
                PrefetchSize = 0,
                PrefetchCount = 1
            };
            _defaultQueueConfig = new QueueConfig
            {
                Durable = false
            };
        }
        
        private void ConfigureChannel(ChannelInfo channel)
        {
            channel.Channel.BasicQos(0, (ushort) channel.Config.PrefetchCount, false);
        }
    }

    struct ChannelInfo
    {
        public IModel Channel { get; set; }
        public ChannelConfig Config { get; set; }
    }
}