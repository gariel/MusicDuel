namespace Application.Common.Messaging;

public delegate void ConsumerDelegate(string content);

public interface IMessageBroker
{
    void Subscribe(ISubscriptionKey key, ConsumerDelegate callback);
    void Publish(ISubscriptionKey key, object content);
    void Unsubscribe(UserRoomSubKey key);
}