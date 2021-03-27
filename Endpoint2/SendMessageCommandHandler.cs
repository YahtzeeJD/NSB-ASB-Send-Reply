using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

public class SendMessageCommandHandler : IHandleMessages<SendMessageCommand>
{
    static readonly ILog log = LogManager.GetLogger<SendMessageCommandHandler>();

    public Task Handle(SendMessageCommand message, IMessageHandlerContext context)
    {
        log.Info($"Received Command: {message.Property}");

        var message2 = new SendMessageResponse
        {
            Property = $"Hello from Endpoint2 at {DateTime.Now:MM/dd/yyyy hh:mm:ss.fff tt}"
        };
        return context.Reply(message2);
    }
}