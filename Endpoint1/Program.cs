using NServiceBus;
using System;
using System.Threading.Tasks;

class Program : EndpointConfiguration
{
    const string endpointName = "Samples.ASBS.SendReply.Endpoint1";

    static async Task Main()
    {
        IEndpointInstance endpointInstance = await Configure(endpointName).ConfigureAwait(false);

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
            var message = new SendMessageCommand
            {
                Property = $"Hello from Endpoint1 at {DateTime.Now:MM/dd/yyyy hh:mm:ss.fff tt}"
            };

            await endpointInstance.Send("Samples.ASBS.SendReply.Endpoint2", message)
                .ConfigureAwait(false);

            Console.WriteLine("Command sent");
        }

        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}