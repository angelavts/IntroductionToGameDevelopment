using System;
using System.Collections.Generic;

public static class EventBus<T>
{
    private static readonly Dictionary<GameEvent, Action<T>> _events = new();

    public static void Subscribe(GameEvent eventType, Action<T> callback)
    {
        if (callback == null) return;

        if (_events.TryGetValue(eventType, out var existing))
            _events[eventType] = existing + callback;
        else
            _events[eventType] = callback;
    }

    public static void Unsubscribe(GameEvent eventType, Action<T> callback)
    {
        if (callback == null) return;

        if (_events.TryGetValue(eventType, out var existing))
        {
            existing -= callback;
            if (existing == null) _events.Remove(eventType);
            else _events[eventType] = existing;
        }
    }

    public static void Publish(GameEvent eventType, T value)
    {
        if (_events.TryGetValue(eventType, out var callbacks))
            callbacks?.Invoke(value);
    }

    public static void ClearAll() => _events.Clear();
}