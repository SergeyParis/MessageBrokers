using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Contracts.Impl;
using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Factories
{
    class ChannelFactory : IDisposable
    {
        private readonly Dictionary<string, IConnection> Connections;
        private readonly Dictionary<string, IChannel> Channels;

        public ChannelFactory()
        {
            Channels = new Dictionary<string, IChannel>();
            Connections = new Dictionary<string, IConnection>();
        }

        /// <summary>
        /// Like connection
        /// </summary>
        public IChannel GetOrCreateChannel(string host, ChannelConfig config)
        {
            var connection = GetConnectionOrCreate(host);
            var channel = GetChannelOrCreate(connection, config);

            return channel;
        }

        private IConnection GetConnectionOrCreate(string host)
        {
            if (Connections.Any(x => x.Key == host))
                return Connections[host];

            var connection = new ConnectionFactory {HostName = host}.CreateConnection();
            Connections.Add(host, connection);

            return connection;
        }

        private IChannel GetChannelOrCreate(IConnection connection, ChannelConfig config)
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

        private void ConfigureChannel(IChannel channel)
        {
            channel.MqChannel.BasicQos(0, (ushort) channel.Config.PrefetchCount, false);
        }

        public void Dispose()
        {
            foreach (var connection in Connections)
                connection.Value.Dispose();
        }
    }
}