using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqSender.model
{
    public class Message
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public IBasicProperties BasicProperties { get; set; }
        public byte[] Body { get; set; }
    }
}
