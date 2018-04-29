using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ThisEvent : UnityEvent<object>
{

}

public class EventManager : MonoBehaviour
{

    Dictionary<string, ThisEvent> eventDictionary;

    static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, ThisEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<object> listener)
    {
        ThisEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ThisEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }
    
    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (eventManager == null) return;
        ThisEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, object value)
    {
        ThisEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(value);
        } else
        {
            Debug.LogError("[EventManager] Event trying to trigger " + eventName + " is not listed. Do you start listening ?");
        }
    }

    public static void TriggerEvent(string eventName, List<object> value)
    {
        ThisEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            Debug.Log("[EventManager] event.Invoke: " + eventName);
            thisEvent.Invoke(value);
        }
        else
        {
            Debug.LogError("[EventManager] Event trying to trigger " + eventName + " is not listed. Do you start listening ?");
        }
    }
}