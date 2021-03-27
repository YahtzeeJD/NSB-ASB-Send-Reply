using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;

public class SendMessageResponseHandler : IHandleMessages<SendMessageResponse>
{
    static readonly ILog log = LogManager.GetLogger<SendMessageResponseHandler>();

    public Task Handle(SendMessageResponse message, IMessageHandlerContext context)
    {
        log.Info($"Received Response: {message.Property}");
        return Task.CompletedTask;
    }
}