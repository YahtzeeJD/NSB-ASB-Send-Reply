using NServiceBus;

public class SendMessageResponse :
    IMessage
{
    public string Property { get; set; }
}