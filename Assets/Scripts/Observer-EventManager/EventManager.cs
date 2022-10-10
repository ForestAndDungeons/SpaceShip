using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    public delegate void EventReceiver(params object[] parameterContainer);
    private static Dictionary<string, EventReceiver> _events;

    public static void SubscribeToEvent(string eventType, EventReceiver listener)
    {
        if (_events==null)
        {
            _events = new Dictionary<string, EventReceiver>();
        }
        if (!_events.ContainsKey(eventType))
        {
            _events.Add (eventType, null);
        }
        _events [eventType] += listener;
    }

    public static void UnsubscribeToEvent(string eventType,EventReceiver listener)
    {
        if (_events!=null)
        {
            if (_events.ContainsKey(eventType))
            {
                _events[eventType] -= listener;
            }
        }
    }
    public static void TriggerEvent(string eventType, params object[] parametersWrapper)
    {
        if (_events == null)
        {
            UnityEngine.Debug.LogWarning("No events subscribed");
            return;
        }

        if (_events.ContainsKey(eventType))
        {
            if (_events[eventType] != null)
                _events[eventType](parametersWrapper);
        }
    }


}
