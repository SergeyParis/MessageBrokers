using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Contracts.Configs;
using Infrastructure.Brokers.RabbitMq.Contracts.Impl;
using Infrastructure.Brokers.RabbitMq.Facades;
using Infrastructure.Brokers.RabbitMq.Facades.Impl;
using Infrastructure.Brokers.RabbitMq.Factories;
using Infrastructure.Brokers.RabbitMq.Providers;
using Infrastructure.Brokers.RabbitMq.Providers.Impl;
using ChannelFactory = Infrastructure.Brokers.RabbitMq.Factories.ChannelFactory;

namespace Infrastructure.Brokers.RabbitMq
{
    public class RabbitMq : IDisposable
    {
        private MqConfig _mqConfig;
        // providers
        private readonly IChannelProvider _channelProvider;
        // facades
        public IQueuesFacade Queues { get; }
        public IMessageFacade Messages { get; }
        public IExchangeFacade Exchange { get; }

        public string Host
        {
            set => _channelProvider.Host = value;
            get => _channelProvider.Host;
        }
        public ChannelConfig ChannelConfig
        {
            set => UpdateConfigs(value);
            get => _mqConfig.ChannelConfig;
        }
        
        /// <summary>
        /// Default host is localhost
        /// </summary>
        public RabbitMq(string host = null, MqConfig? config = null)
        {
            InitConfigs(config);
            
            _channelProvider = new ChannelProvider(host, _mqConfig.ChannelConfig);

            Queues = new QueuesFacade(_channelProvider, _mqConfig.QueueConfig);
            Messages = new MessageFacade(_channelProvider);
            Exchange = new ExchangeFacade(_channelProvider, _mqConfig.ExchangeConfig);
        }

        private void InitConfigs(MqConfig? @in)
        {
            _mqConfig = @in ?? new MqConfig
            {
                ChannelConfig = ConfigFactory.GetChannelConfig(),
                ExchangeConfig = ConfigFactory.GetExchangeConfig(),
                QueueConfig = ConfigFactory.GetQueueConfig()
            };
        }

        private void UpdateConfigs(ChannelConfig value)
        {
            _mqConfig.ChannelConfig = value;
            _channelProvider.ChannelConfig = value;
        }
        
        public void Dispose()
        {
            ChannelFactory.Dispose();
        }
    }
}