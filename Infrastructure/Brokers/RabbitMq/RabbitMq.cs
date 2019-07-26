using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Facades;
using Infrastructure.Brokers.RabbitMq.Facades.Impl;
using Infrastructure.Brokers.RabbitMq.Factories;
using Infrastructure.Brokers.RabbitMq.Models;
using ChannelFactory = Infrastructure.Brokers.RabbitMq.Factories.ChannelFactory;

namespace Infrastructure.Brokers.RabbitMq
{
    public class RabbitMq : IDisposable
    {
        // providers
        private readonly IChannelProvider _channelProvider;
        // facades
        private readonly IQueuesFacade _queuesFacade;
        
        public string Host
        {
            set => _channelProvider.Host = value;
            get => _channelProvider.Host;
        }
        public ChannelConfig? ChannelConfig
        {
            set => _channelProvider.ChannelConfig = value;
            get => _channelProvider.ChannelConfig;
        }
        
        /// <summary>
        /// Default host is localhost
        /// </summary>
        public RabbitMq(string host = null, ChannelConfig? config = null)
        {
            _channelProvider = new ChannelProvider(host, config);
            _queuesFacade = new QueuesFacade(_channelProvider.GetChannel(), ConfigFactory.GetQueueConfig());
        }

        public IQueuesFacade Queues() => _queuesFacade;
        
        public void Dispose()
        {
            ChannelFactory.Dispose();
        }
    }
}