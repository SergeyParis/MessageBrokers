using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Factories;
using Infrastructure.Brokers.RabbitMq.Models;

namespace Infrastructure.Brokers.RabbitMq
{
    public class RabbitMq : IDisposable
    {
        private const string LocalHostName = "localhost";

        private static readonly ChannelFactory FactoryChannel;
        
        static RabbitMq()
        {
            FactoryChannel = new ChannelFactory();
        }
        
        public static IChannel GetOrCreateChannel(string host = LocalHostName, ChannelConfig? config = null)
        {
            if (config == null)
                config = ConfigFactory.GetChannelConfig();
            
            return FactoryChannel.GetOrCreateChannel(host, config.Value);
        }

        public void Dispose()
        {
            FactoryChannel.Dispose();
        }
    }
}