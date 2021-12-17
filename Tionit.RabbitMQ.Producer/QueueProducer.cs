using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Tionit.MassTransit.Consumer.Models;

namespace Tionit.RabbitMQ.Producer
{
    public class QueueProducer
    {

        public static void Publish(IModel channel)
        {
            
            channel.QueueDeclare("ConsumerExchange",
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null );

            Console.Write("Нажмите Enter  для продолжения .... ");
            while (Console.ReadLine() != "exit")
            {
                Console.Write("Введите имя клиента .... ");
                var Name = Console.ReadLine();
                Console.Write("Введите возраст .... ");
                if (!int.TryParse(Console.ReadLine(), out int Age))
                {
                    Console.WriteLine("Invalid value entered");
                }
                else
                {
                    var message = new CustomerModel { CustomerName = Name, CustomerAge = Age };
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    try
                    {
                        channel.BasicPublish("ConsumerQueue", "partners.queue", null, body);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }

                    Console.WriteLine($"Name:{message.CustomerName} Age:{message.CustomerAge} - success published to partners.queue");
                }
            }
        }
            
           
            
           


         
    }
}
