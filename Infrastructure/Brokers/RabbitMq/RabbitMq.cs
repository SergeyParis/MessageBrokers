using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Factories;
using Infrastructure.Brokers.RabbitMq.Models;

namespace Infrastructure.Brokers.RabbitMq
{
    public class RabbitMq : IDisposable
    {
        private const string LocalHostName = "localhost";

        private readonly ConfigFactory _configFactory;
        private readonly ChannelFactory _channelFactory;
        private readonly QueueFactory _queueFactory;
        
        private string _hostName;
        private ChannelConfig _channelConfig;

        public string Host
        {
            set => _hostName = value ?? LocalHostName;
            get => _hostName;
        }
        public ChannelConfig? ChannelConfig
        {
            set => _channelConfig = value ?? _configFactory.GetChannelConfig();
            get => _channelConfig;
        }

        public RabbitMq(string host = LocalHostName, ChannelConfig? config = null)
        {
            Host = host;
            ChannelConfig = config;

            _configFactory = new ConfigFactory();
            _channelFactory = new ChannelFactory();
            _queueFactory = new QueueFactory(GetChannel(), _configFactory.GetQueueConfig());
        }

        private IChannel GetChannel() => _channelFactory.GetOrCreateChannel(Host, ChannelConfig.Value);
        
        public void Dispose()
        {
            _channelFactory.Dispose();
        }
    }
}