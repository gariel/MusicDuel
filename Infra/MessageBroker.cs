using System.Collections.Concurrent;
using System.Text.Json;
using Application.Common.Messaging;

namespace Infra;

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

        typeRegister.TryRemove(key.Parse(), out _);
        if (!typeRegister.TryAdd(key.Parse(), callback))
            throw new Exception("error registering consumer");
    }

    public void Publish(ISubscriptionKey key, object content)
    {
        if (!_register.TryGetValue(key.GetType(), out var typeRegister))
            throw new Exception("error recovering registers");

        if (!typeRegister.TryGetValue(key.Parse(), out var callback))
            throw new Exception("error recovering consumer");

        callback(JsonSerializer.Serialize(new DefaultMessage
        {
            Type = content.GetType().FullName ?? content.GetType().Name,
            Content = JsonSerializer.Serialize(content),
        }));
    }

    public void Unsubscribe(UserRoomSubKey key)
    {
        if (!_register.TryGetValue(key.GetType(), out var typeRegister))
        {
            typeRegister = new ConcurrentDictionary<string, ConsumerDelegate>();
            if (!_register.TryAdd(key.GetType(), typeRegister))
                throw new Exception("error registering consumers");
        }
        typeRegister.TryRemove(key.Parse(), out _);
    }

    public class DefaultMessage
    {
        public string Type { get; set; }
        public string Content { get; set; }
    }
}