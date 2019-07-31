using ExchangeType = Infrastructure.Brokers.RabbitMq.Contracts.Enums.ExchangeType;

namespace Infrastructure.Brokers.RabbitMq.Facades
{
    public interface IExchangeFacade
    {
        void CreateExchange(string name, ExchangeType type);
    }
}