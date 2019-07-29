using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Facades
{
    internal abstract class BaseFacade
    {
        protected readonly IChannelProvider ChannelProvider;

        protected IModel MqChannel => GetMqChannel();
        protected ChannelConfig MqChannelConfig => GetMqChannelConfig();
        
        public BaseFacade(IChannelProvider channelProvider)
        {
            if (channelProvider == null)
                throw new NullReferenceException();
            
            ChannelProvider = channelProvider;
        }
        
        private IModel GetMqChannel() => ChannelProvider.GetChannel().MqChannel;
        private ChannelConfig GetMqChannelConfig() => ChannelProvider.GetChannel().Config;
    }
}