using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Facades.Interfaces;

namespace Infrastructure.Brokers.RabbitMq.Facades
{
    class ExchangeFacade : BaseFacade, IExchangeFacade
    {
        public ExchangeFacade(IChannelProvider channelProvider) : base(channelProvider)
        {
        }
    }
}