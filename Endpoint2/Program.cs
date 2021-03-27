using NServiceBus;
using System;
using System.Threading.Tasks;

class Program : EndpointConfiguration
{
    const string endpointName = "Samples.ASBS.SendReply.Endpoint2";

    static async Task Main()
    {
        IEndpointInstance endpointInstance = await Configure(endpointName).ConfigureAwait(false);

        Console.Title = endpointName;
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();

        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}