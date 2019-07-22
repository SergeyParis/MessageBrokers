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

        public static IChannel GetOrCreateChannel(string host = LocalHostName, ChannelConfig? config = null)
        {
            return null;
        }
    }
}