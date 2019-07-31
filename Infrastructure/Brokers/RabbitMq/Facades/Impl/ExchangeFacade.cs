using System;
using Infrastructure.Brokers.RabbitMq.Contracts;
using RabbitMQ.Client;
using ExchangeTypeEnum = Infrastructure.Brokers.RabbitMq.Contracts.Enums.ExchangeType;

namespace Infrastructure.Brokers.RabbitMq.Facades.Impl
{
    class ExchangeFacade : BaseFacade, IExchangeFacade
    {
        public ExchangeFacade(IChannelProvider channelProvider) : base(channelProvider)
        {
        }

        public void CreateExchange(string name, ExchangeTypeEnum type)
        {
            MqChannel.ExchangeDeclare(name, ConvertToMqExchangeType(type));
        }
        
        public void CreateExchange(string name, ExchangeTypeEnum type, bool durable, bool autoDelete)
        {
            MqChannel.ExchangeDeclare(name, ConvertToMqExchangeType(type), durable, autoDelete, null);
        }
        
        private string ConvertToMqExchangeType(ExchangeTypeEnum type)
        {
            switch (type)
            {
                case ExchangeTypeEnum.Direct: return RabbitMQ.Client.ExchangeType.Direct;
                case ExchangeTypeEnum.Topic: return RabbitMQ.Client.ExchangeType.Topic;
                case ExchangeTypeEnum.Headers: return RabbitMQ.Client.ExchangeType.Headers;
                case ExchangeTypeEnum.Fanout: return RabbitMQ.Client.ExchangeType.Fanout;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}