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
        private List<string> _listQueues = new List<string>();
        public RabbitConnection(RabbitConfig config) 
        {
            _factory = new ConnectionFactory()
            {
                UserName = config.UserName,
                Password = config.Password,
                VirtualHost = config.VirtualHost,
                HostName = config.HostName,
                Port = config.Port
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
        public async Task<List<string>> GetQueues(RabbitConfig config)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = $"http://{config.HostName}:15672/api/queues";

                    var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.UserName}:{config.Password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                    var teste = $"Authorization: Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.UserName}:{config.Password}"))}";
                    HttpResponseMessage response = await client.GetAsync(uri);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    var queues = JsonConvert.DeserializeObject<List<dynamic>>(responseBody);

                    var queueNames = new List<string>();
                    foreach (var queue in queues)
                    {
                        queueNames.Add(queue.name.ToString());
                    }

                    return queueNames;
                }
                catch (HttpRequestException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
