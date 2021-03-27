using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

public class Message2Handler :
    IHandleMessages<SendMessageResponse>
{
    static readonly ILog log = LogManager.GetLogger<Message2Handler>();

    public Task Handle(SendMessageResponse message, IMessageHandlerContext context)
    {
        log.Info($"Received Response: {message.Property}");
        return Task.CompletedTask;
    }
}