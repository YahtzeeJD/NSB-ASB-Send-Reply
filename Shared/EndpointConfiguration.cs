using NServiceBus;
using System;
using System.Threading.Tasks;

public class EndpointConfiguration
{
    public static async Task<IEndpointInstance> Configure(string endpointName)
    {
        var endpointConfiguration = new NServiceBus.EndpointConfiguration(endpointName);
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