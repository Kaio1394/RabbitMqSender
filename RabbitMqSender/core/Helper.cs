using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RabbitMqSender.core
{
    public static class Helper
    {
        private static List<string> _listQeues = new List<string>();
        public static async Task<List<string>> GetQueues(string host, string username, string password)
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
                    Console.WriteLine("Filas disponíveis:");
                    Console.WriteLine(responseBody);

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

        public static void CreateQeue()
        {

        }
    }
}
