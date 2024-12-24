using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMqSender.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;

namespace RabbitMqSender.core
{
    public class RabbitConnection
    {
        public readonly bool channelSuccess = false;
        public readonly bool connectionSuccess = false;
        private ConnectionFactory _factory;
        private IConnection _iConnection;
        private IModel _iChannel;
        private List<string> _listQeues = new List<string>();
        public RabbitConnection(RabbitConfig config) 
        {
            _factory = new ConnectionFactory()
            {
                UserName = config.UserName,
                Password = config.Password,
                VirtualHost = config.VirtualHost,
                HostName = config.HostName,
            };
            connectionSuccess = CreateConnection();
            channelSuccess = CreateChannel();
        }
        private bool CreateConnection()
        {
            try
            {
                _iConnection = _factory.CreateConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool CreateChannel()
        {
            try
            {
                _iChannel = _iConnection.CreateModel();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChannelIsOpen()
        {
            if (connectionSuccess && channelSuccess)
                return _iChannel.IsOpen;
            return false;
        }
        public void CreateQueue(Queue queue)
        {
            _iChannel.QueueDeclare(
                queue: queue.Name, 
                durable: queue.Durable,  
                exclusive: queue.Exclusive,
                autoDelete: queue.AutoDelete,
                arguments: queue.Arguments
            );
        }
        public void SendMessage(Message message)
        {
            _iChannel.BasicPublish(
                exchange: message.Exchange,
                routingKey: message.RoutingKey,
                basicProperties: message.BasicProperties,
                body: message.Body
            );
        }
        public async Task<List<string>> GetQueues(string host, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                var byteArray = new System.Text.UTF8Encoding().GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                try
                {
                    HttpResponseMessage response = await client.GetAsync(host);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var queues = JsonConvert.DeserializeObject<List<dynamic>>(responseBody);

                    foreach (var queue in queues)
                    {
                        _listQeues.Add(queue.name);
                    }
                    return _listQeues;
                }
                catch (Exception ex)
                {
                    return new List<string>();
                }
            }
        }
    }
}
