using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Factories;
using Infrastructure.Brokers.RabbitMq.Models;

namespace Infrastructure.Brokers.RabbitMq
{
    public class RabbitMq : IDisposable
    {
        private const string LocalHostName = "localhost";

        private readonly ChannelFactory _channelFactory;

        private string _hostName;
        private ChannelConfig _channelConfig;

        public string Host
        {
            set => _hostName = value ?? LocalHostName;
            get => _hostName;
        }

        public ChannelConfig? ChannelConfig
        {
            set => _channelConfig = value ?? ConfigFactory.GetChannelConfig();
            get => _channelConfig;
        }

        public RabbitMq(string host = LocalHostName, ChannelConfig? config = null)
        {
            Host = host;
            ChannelConfig = config;

            _channelFactory = new ChannelFactory();
        }

        private IChannel GetOrCreateChannel() => _channelFactory.GetOrCreateChannel(Host, ChannelConfig.Value);

        public void Dispose()
        {
            _channelFactory.Dispose();
        }
    }
}