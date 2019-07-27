using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq
{
    [Obsolete]
    public class RabbitMqClient_COPY
    {
        public void PublishMessage(byte[] body, string routingKey)
        {
            GetChannelInfo().Channel.BasicPublish("", routingKey, null, body);
        }

        public void Ack(BasicDeliverEventArgs args)
        {
            var channel = GetChannelInfo().Channel;
            channel.BasicAck(args.DeliveryTag, false);
        }

        public byte[] WaitMessageFromQueue(string queueName)
        {
            var channelInfo = GetChannelInfo();

            BasicGetResult result = null;
            while (result == null)
            {
                result = channelInfo.Channel.BasicGet(queueName, channelInfo.Config.AutoAck);
                Thread.Sleep(50);
            }

            return result.Body;
        }
      
    }

}