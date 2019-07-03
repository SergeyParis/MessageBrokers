using System;
using System.Threading;
using Infrastructure;
using Infrastructure.Brokers.RabbitMq;
using Producer.Data;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var messageProvider = MessageProviderFactory.GetProvider(ProviderType.Array);

            var data = new []
            {
                "First message.", 
                "Second message..", 
                "Third message...", 
                "Fourth message....", 
                "Fifth message....."
            };
            
            while (true)
            {
                var rabbit = new RabbitMqClient();

                var message = messageProvider.GetMessage(data);
                
                Console.WriteLine($"Publish: {message}");
                rabbit.PublishMessage(message.TransformToByte());
                
                Thread.Sleep(300);
            }
        }
    }
}