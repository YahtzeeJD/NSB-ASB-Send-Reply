using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    const string endpointName = "Samples.ASBS.SendReply.Endpoint1";

    static async Task Main()
    {
        IEndpointInstance endpointInstance = await ConfigureEndpoint().ConfigureAwait(false);

        Console.Title = endpointName;
        Console.WriteLine("Press 'enter' to send a message");
        Console.WriteLine("Press any other key to exit");

        while (true)
        {
            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key != ConsoleKey.Enter)
            {
                break;
            }

            var orderId = Guid.NewGuid();
            var message = new Message1
            {
                Property = $"Hello from Endpoint1 at {DateTime.Now:MM/dd/yyyy hh:mm:ss.fff tt}"
            };

            await endpointInstance.Send("Samples.ASBS.SendReply.Endpoint2", message)
                .ConfigureAwait(false);

            Console.WriteLine("Message1 sent");
        }

        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }

    private static async Task<IEndpointInstance> ConfigureEndpoint()
    {
        var endpointConfiguration = new EndpointConfiguration(endpointName);
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();

        var connectionString = Environment.GetEnvironmentVariable("AzureServiceBus_ConnectionString");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("Could not read the 'AzureServiceBus_ConnectionString' environment variable. Check the sample prerequisites.");
        
        transport.ConnectionString(connectionString);

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

        return endpointInstance;
    }
}