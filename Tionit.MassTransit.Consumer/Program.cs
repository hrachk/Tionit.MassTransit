// See https://aka.ms/new-console-template for more information
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Tionit.MassTransit.Consumer.Models;

namespace Tionit.MassTransit.Consumer;

public class Program
{
    private readonly IServiceCollection _services;
    public Program(IServiceCollection services)
    {
        _services = services; 
    }

    public static async Task Main()
    {
        var busControl = Bus.Factory.CreateUsingRabbitMq();

        var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        await busControl.StartAsync(source.Token);
        try
        {
            while (true)
            {
                string value = await Task.Run(() =>
                {
                    Console.WriteLine("Enter message (or quit to exit)");
                    Console.Write("> ");
                    return Console.ReadLine();
                });

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;

                await busControl.Publish<ValueEntered>(new
                {
                    Value = value
                });
            }
        }
        finally
        {
            await busControl.StopAsync();
        }
    }
}

 