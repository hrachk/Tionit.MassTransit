using MassTransit;
using MassTransit.MessageData;
using Tionit.MassTransit.Consumer.Entities;
using Tionit.MassTransit.Consumer.Models;

namespace Tionit.MassTransit.Consumer;
 
public class SubmitOrder
{
    public string Text { get; set; }
}
public class SubmitOrderConsumer : IConsumer<CustomerModel>
{
    public async Task Consume(ConsumeContext<CustomerModel> context)
    {
        #region Entities 
          using (var datacontext = new DataContext())
          {
            datacontext.Add(new CustomerModel { CustomerName = context.Message.CustomerName, CustomerAge = context.Message.CustomerAge });

             await   datacontext.SaveChangesAsync();

              Console.WriteLine($"Customer added to database:");
          }   
        #endregion
       await Console.Out.WriteLineAsync($"Name:{context.Message.CustomerName} Age:{context.Message.CustomerAge} - success added to database");
       //    await Task.CompletedTask;
    }
}
 

public class Program
{

    public static async Task Main()
    {
        try
        {
         await StartFactoryAsync();

        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message); 
        }
         
    }

    private static async Task StartFactoryAsync()
    {
        IMessageDataRepository messageDataRepository = new InMemoryMessageDataRepository();
        var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
         {
             cfg.Host("localhost", "/", u =>
             {
                 u.Username("guest");
                 u.Password("guest");
             });

             cfg.UseMessageData(messageDataRepository);

             cfg.ReceiveEndpoint(queueName: "ConsumerQueue", e =>
             {
                 e.PrefetchCount = 20;
                 e.ConfigureConsumeTopology = false;
                 e.Consumer<SubmitOrderConsumer>();
                 e.ClearMessageDeserializers();
                 e.UseRawJsonSerializer();

                 e.Bind("ConsumerExchange", x =>
                 {
                     x.Durable = true;
                     x.AutoDelete = false;
                     x.ExchangeType = "fanout";
                 });

             });
             
         });  
       
        await busControl.StartAsync();
        try
        {
            await Task.Run(() => Console.ReadLine());
        }
        catch (Exception e)
        {
            await Task.Run(() => Console.WriteLine(e.Message));
        }
    }
}
