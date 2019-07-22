using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Contracts.Impl;
using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq
{
    public static class RabbitMq
    {
        private const string LocalHostName = "localhost";

        private static readonly Dictionary<string, IConnection> Connections;
        private static readonly Dictionary<string, IChannel> Channels;

        static RabbitMq()
        {
            Channels = new Dictionary<string, IChannel>();
            Connections = new Dictionary<string, IConnection>();
        }
        
        /// <summary>
        /// Like connection
        /// </summary>
        public static IChannel GetChannel(string host = LocalHostName, ChannelConfig? config = null)
        {
            var connection = GetConnectionOrCreate(host);

            if (config == null)
                config = new ChannelConfig
                {
                    AutoAck = false,
                    PrefetchSize = 0,
                    PrefetchCount = 1
                };

            var channel = GetChannelOrCreate(connection, config.Value);
            return channel;
        }

        private static IConnection GetConnectionOrCreate(string host)
        {
            if (Connections.Any(x => x.Key == host))
                return Connections[host];
            
            var connection = new ConnectionFactory { HostName = host }.CreateConnection();
            Connections.Add(host, connection);

            return connection;
        }
        
        private static IChannel GetChannelOrCreate(IConnection connection, ChannelConfig config)
        {
            // channel - program connection to Rabbit MQ (new channel must create for new threads)
            // auto-create new channel for every new thread
            var channelName = Thread.CurrentThread.ManagedThreadId.ToString();
            IChannel contractChannel;

            if (Channels.Any(x => x.Key == channelName))
                contractChannel = Channels[channelName];
            else
            {
                contractChannel = new Channel
                {
                    MqChannel = connection.CreateModel(),
                    Config = config
                };
                
                Channels.Add(channelName, contractChannel);
            }
            
            ConfigureChannel(contractChannel);
            return contractChannel;
        }
        
        private static void ConfigureChannel(IChannel channel)
        {
            channel.MqChannel.BasicQos(0, (ushort) channel.Config.PrefetchCount, false);
        }
    }
}