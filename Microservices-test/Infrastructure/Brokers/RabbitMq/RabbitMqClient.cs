using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Infrastructure.Brokers.RabbitMq.Config;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq
{
    public class RabbitMqClient : IDisposable
    {
        private readonly IConnection _connection;
        private readonly Dictionary<string, IModel> _channels;

        
        public bool AutoAck { get; set; } = true;
        
        public ChannelConfig DefaultChannelConfig { get; set; }
        public QueueConfig DefaultQueueConfig { get; set; }

        public RabbitMqClient(string host = "localhost")
        {
            _channels = new Dictionary<string, IModel>();
            InitDefaultConfig();
            
            var factory = new ConnectionFactory {HostName = host};
            _connection = factory.CreateConnection();
        }

        public void PublishMessage(byte[] body, string routingKey)
        {
            GetChannel().BasicPublish("", routingKey, null, body);
        }

        public void SubscribeOnQueue(Action<object, BasicDeliverEventArgs> handler, string queueName)
        {
            var channel = GetChannel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, args) => handler(model, args);

            channel.BasicConsume(queueName, AutoAck, consumer);
        }

        public void Ack(BasicDeliverEventArgs args)
        {
            var channel = GetChannel();
            channel.BasicAck(args.DeliveryTag, false);
        }

        public byte[] WaitMessageFromQueue(string queueName)
        {
            var channel = GetChannel();

            BasicGetResult result = null;
            while (result == null)
            {
                result = channel.BasicGet(queueName, AutoAck);
                Thread.Sleep(50);
            }

            return result.Body;
        }

        public void CreateQueue(string queueName, QueueConfig queueConfig = null)
        {
            var config = queueConfig ?? DefaultQueueConfig;
            GetChannel().QueueDeclare(queueName, config.IsDurable, false, false, null);
        }
        
        public void Dispose()
        {
            _connection?.Dispose();
            foreach (var channel in _channels)
                channel.Value.Dispose();
        }
        
        private IModel GetChannel()
        {
            // channel - program connection to Rabbit MQ (new channel must create for new threads)
            // auto-create new channel for every new thread
            var channelName = Thread.CurrentThread.ManagedThreadId.ToString();
            IModel channel;

            if (_channels.Any(x => x.Key == channelName))
                channel = _channels[channelName];
            else
            {
                channel = _connection.CreateModel();
                _channels.Add(channelName, channel);
            }
            
            ConfigureChannel(channel);
            return channel;
        }

        private void InitDefaultConfig()
        {
            DefaultChannelConfig = new ChannelConfig
            {
                PrefetchSize = 0,
                PrefetchCount = 1
            };
            DefaultQueueConfig = new QueueConfig
            {
                IsDurable = false
            };
        }
        
        private void ConfigureChannel(IModel channel, ChannelConfig channelConfig = null)
        {
            var config = channelConfig ?? DefaultChannelConfig;
            channel.BasicQos(0, (ushort) config.PrefetchCount, false);
        }
    }

//    struct ChannelInfo
//    {
//        public IModel Channel { get; set; }
//
//        public ChannelConfig Config { get; set; }
//    }
}