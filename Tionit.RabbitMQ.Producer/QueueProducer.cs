using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Tionit.RabbitMQ.Producer
{
    public class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("Tionit-queue1",
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null ); 
 
                var message = new { Name = "Producer", Age = "29" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "demo-queue", null, body);
            
        }
    }
}
