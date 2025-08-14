using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class SimpleEventBus
{
    // Diccionario: TipoDeEvento -> lista de callbacks
    private static readonly Dictionary<GameEvent, Action> _events = new();

    public static void Subscribe(GameEvent eventType, Action callback)
    {
        if (callback == null) return;

        if (_events.TryGetValue(eventType, out var existing))
            _events[eventType] = existing + callback;     // agrega al multicast
        else
            _events[eventType] = callback;                 // primera suscripción
    }

    public static void Unsubscribe(GameEvent eventType, Action callback)
    {
        if (callback == null) return;

        if (_events.TryGetValue(eventType, out var existing))
        {
            existing -= callback;
            if (existing == null) _events.Remove(eventType); // limpia si no quedan
            else _events[eventType] = existing;
        }
    }

    public static void Publish(GameEvent eventType)
    {
        if (_events.TryGetValue(eventType, out var callbacks))
            callbacks?.Invoke();
    }

    // Útil para tests/demos
    public static void ClearAll() => _events.Clear();
}
