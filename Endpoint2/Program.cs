using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    const string endpointName = "Samples.ASBS.SendReply.Endpoint2";

    static async Task Main()
    {
        IEndpointInstance endpointInstance = await ConfigureEndpoint().ConfigureAwait(false);

        Console.Title = endpointName;
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();

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