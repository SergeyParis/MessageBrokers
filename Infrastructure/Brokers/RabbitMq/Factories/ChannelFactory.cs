using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Factories
{
    static class ChannelFactory
    {
        private static readonly Dictionary<string, IConnection> _connections;
        private static readonly Dictionary<string, IChannel> _channels;

        static ChannelFactory()
        {
            _channels = new Dictionary<string, IChannel>();
            _connections = new Dictionary<string, IConnection>();
        }

        /// <summary>
        /// Like connection
        /// </summary>
        public static IChannel GetChannel(string host, ChannelConfig config)
        {
            var connection = GetConnectionOrCreate(host);
            var channel = GetOrCreateChannel(connection, config);

            return channel;
        }

        private static IConnection GetConnectionOrCreate(string host)
        {
            if (_connections.Any(x => x.Key == host))
                return _connections[host];

            var connection = new ConnectionFactory {HostName = host}.CreateConnection();
            _connections.Add(host, connection);

            return connection;
        }

        private static IChannel GetOrCreateChannel(IConnection connection, ChannelConfig config)
        {
            // channel - program connection to Rabbit MQ (new channel must create for new threads)
            // auto-create new channel for every new thread
            var channelName = Thread.CurrentThread.ManagedThreadId.ToString();
            IChannel contractChannel;

            if (_channels.Any(x => x.Key == channelName))
                contractChannel = _channels[channelName];
            else
            {
                contractChannel = new Channel
                {
                    MqChannel = connection.CreateModel(),
                    Config = config
                };

                _channels.Add(channelName, contractChannel);
            }

            ConfigureChannel(contractChannel);
            return contractChannel;
        }

        private static void ConfigureChannel(IChannel channel)
        {
            channel.MqChannel.BasicQos(0, (ushort) channel.Config.PrefetchCount, false);
        }

        public static void Dispose()
        {
            foreach (var connection in _connections)
                connection.Value.Dispose();
        }
    }
}