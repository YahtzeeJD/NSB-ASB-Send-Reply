using NServiceBus;

public class SendMessageCommand :
    IMessage
{
    public string Property { get; set; }
}