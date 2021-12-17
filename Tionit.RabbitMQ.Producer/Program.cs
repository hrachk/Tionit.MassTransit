// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using Tionit.RabbitMQ.Producer;

    Console.WriteLine("Producer started..");

var factory = new ConnectionFactory
{
      Uri = new Uri(uriString:"amqp://guest:guest@localhost:5672")     

};
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();


  channel.ExchangeDeclare("ConsumerExchange", ExchangeType.Fanout,true, false);
  
  QueueProducer.Publish(channel);

 //channel.QueueBind("partners.queue", "partners.exchange", "from partners", null);
