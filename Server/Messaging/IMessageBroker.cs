using System.Collections.Concurrent;
using System.Text.Json;

namespace Server.Messaging;

public delegate void ConsumerDelegate(string content);

public interface IMessageBroker
{
    void Subscribe(ISubscriptionKey key, ConsumerDelegate callback);
    void Publish(ISubscriptionKey key, object content);
}

public class MessageBroker : IMessageBroker
{
    private static ConcurrentDictionary<Type, ConcurrentDictionary<string, ConsumerDelegate>> _register = new();
    
    public void Subscribe(ISubscriptionKey key, ConsumerDelegate callback)
    {
        if (!_register.TryGetValue(key.GetType(), out var typeRegister))
        {
            typeRegister = new ConcurrentDictionary<string, ConsumerDelegate>();
            if (!_register.TryAdd(key.GetType(), typeRegister))
                throw new Exception("error registering consumers");
        }

        if (!typeRegister.TryAdd(key.Parse(), callback))
            throw new Exception("error registering consuner");
    }

    public void Publish(ISubscriptionKey key, object content)
    {
        if (!_register.TryGetValue(key.GetType(), out var typeRegister))
            throw new Exception("error recovering registers");

        if (!typeRegister.TryGetValue(key.Parse(), out var callback))
            throw new Exception("error recovering consuner");

        callback(JsonSerializer.Serialize(new DefaultMessage
        {
            Type = content.GetType().Name,
            Content = JsonSerializer.Serialize(content),
        }));
    }

    public class DefaultMessage
    {
        public string Type { get; set; }
        public string Content { get; set; }
    }
}