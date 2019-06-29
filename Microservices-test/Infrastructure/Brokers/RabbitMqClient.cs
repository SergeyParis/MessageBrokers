using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers
{
    public class RabbitMqClient : IDisposable
    {
        private const string DefaultChannelName = "Default";

        private readonly IConnection _connection;
        private readonly List<IModel> _channels;

        public RabbitMqClient(string host = "localhost")
        {
            _channels = new List<IModel>();

            var factory = new ConnectionFactory {HostName = host};
            _connection = factory.CreateConnection();

            CreateDefaultChannel();
        }

        public void PublishMessage(string message, string queue = DefaultChannelName)
        {
            var channel = GetChannel(queue);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", queue, null, body);
        }

        public void SubscribeOnQueue(Action<object, BasicDeliverEventArgs> handler, string queue = DefaultChannelName)
        {
            var channel = GetChannel(queue);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, args) => handler(model, args);

            channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
        }

        public byte[] WaitMessageFromQueue(string queue = DefaultChannelName)
        {
            var channel = GetChannel(queue);
            var consumer = new EventingBasicConsumer(channel);
            
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            var message = new byte[0];
            consumer.Received += (model, args) =>
            {
                waitHandle.Set();
                message = args.Body;
            };

            waitHandle.WaitOne();
            return message;
        }

        public Task<string> WaitMessageFromQueueAsync(string queue = DefaultChannelName)
        {
            return null;
        }

        public void Dispose()
        {
            _connection?.Dispose();
            foreach (var channel in _channels)
                channel.Dispose();
        }

        private void CreateDefaultChannel() => CreateChannel(DefaultChannelName);

        private void CreateChannel(string channelName)
        {
            var channel = _connection.CreateModel();
            _channels.Add(channel);

            channel.QueueDeclare(queue: channelName, false, false, false, null);
        }

        private IModel GetChannel(string queueName)
        {
            if (queueName != DefaultChannelName)
                throw new NotImplementedException();

            return _channels.First();
        }
    }
}