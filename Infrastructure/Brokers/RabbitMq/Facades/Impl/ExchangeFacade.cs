using Infrastructure.Brokers.RabbitMq.Contracts;

namespace Infrastructure.Brokers.RabbitMq.Facades.Impl
{
    class ExchangeFacade : BaseFacade, IExchangeFacade
    {
        public ExchangeFacade(IChannelProvider channelProvider) : base(channelProvider)
        {
        }

        public void CreateExchange()
        {
            throw new System.NotImplementedException();
        }
    }
}