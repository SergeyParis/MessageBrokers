using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Contracts.Enums;

namespace Infrastructure.Brokers.RabbitMq.Facades.Impl
{
    class ExchangeFacade : BaseFacade, IExchangeFacade
    {
        public ExchangeFacade(IChannelProvider channelProvider) : base(channelProvider)
        {
        }

        public void CreateExchange(string name, ExchangeType type)
        {
            throw new System.NotImplementedException();
        }
    }
}