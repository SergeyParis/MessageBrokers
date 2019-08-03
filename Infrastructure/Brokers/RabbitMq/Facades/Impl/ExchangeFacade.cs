using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Contracts.Configs;
using Infrastructure.Brokers.RabbitMq.Providers;
using RabbitMQ.Client;
using ExchangeTypeEnum = Infrastructure.Brokers.RabbitMq.Contracts.Enums.ExchangeType;

namespace Infrastructure.Brokers.RabbitMq.Facades.Impl
{
    internal class ExchangeFacade : BaseFacade, IExchangeFacade
    {
        public ExchangeConfig Config { get; set; }

        public ExchangeFacade(IChannelProvider channelProvider, ExchangeConfig config) : base(channelProvider)
        {
            Config = config;
        }

        public void CreateExchange(string name, ExchangeTypeEnum type)
        {
            MqChannel.ExchangeDeclare(name, GetExchangeTypeByEnum(type), Config.Durable, Config.AutoDelete, null);
        }
        
        private string GetExchangeTypeByEnum(ExchangeTypeEnum type)
        {
            switch (type)
            {
                case ExchangeTypeEnum.Direct: return ExchangeType.Direct;
                case ExchangeTypeEnum.Topic: return ExchangeType.Topic;
                case ExchangeTypeEnum.Headers: return ExchangeType.Headers;
                case ExchangeTypeEnum.Fanout: return ExchangeType.Fanout;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}