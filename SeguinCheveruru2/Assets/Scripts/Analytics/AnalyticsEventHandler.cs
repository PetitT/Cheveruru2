using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsEventHandler : MonoBehaviour
{
    public List<CustomUntypedAnalyticsEvent> analyticsEvents = new List<CustomUntypedAnalyticsEvent>();
    public List<CustomGenericAnalyticsEvent<float>> floatAnalyticsEvents = new List<CustomGenericAnalyticsEvent<float>>();
    public List<CustomGenericScriptableValueEvent<float>> floatValues = new List<CustomGenericScriptableValueEvent<float>>();
    public List<CustomGenericScriptableValueEvent<int>> intValues = new List<CustomGenericScriptableValueEvent<int>>();
    public List<CustomGenericScriptableValueEvent<string>> stringValues = new List<CustomGenericScriptableValueEvent<string>>();

    private void Awake()
    {
        foreach (var analyticEvent in analyticsEvents)
        {
            analyticEvent.gameEvent.onEventRaised += analyticEvent.SendEvent;
        }

        SubscribeEvents(floatAnalyticsEvents);
        SubscribeToScriptableValueChange(floatValues);
        SubscribeToScriptableValueChange(intValues);
        SubscribeToScriptableValueChange(stringValues);
    }

    private void SubscribeEvents<T>(List<CustomGenericAnalyticsEvent<T>> customEvents)
    {
        foreach (var evt in customEvents)
        {
            evt.gameEvent.onEventRaised += evt.SendEvent;
        }
    }

    private void SubscribeToScriptableValueChange<T>(List<CustomGenericScriptableValueEvent<T>> values)
    {
        foreach (var evt in values)
        {
            evt.gameEvent.onValueChanged += evt.SendEvent;
        }
    }
}

[System.Serializable]
public class CustomUntypedAnalyticsEvent 
{
    public GameEvent gameEvent;
    public string eventName;

    public void SendEvent()
    {
        Analytics.CustomEvent(eventName);
    }
}

[System.Serializable]
public class CustomGenericAnalyticsEvent<T> 
{
    public GenericGameEvent<T> gameEvent;
    public string eventName;
    public string parameterName;

    public void SendEvent(T parameters)
    {
        Dictionary<string, object> eventData = new Dictionary<string, object>();
        eventData.Add(parameterName, parameters);
        Analytics.CustomEvent(eventName, eventData);
        AnalyticsEvent.Custom(eventName, eventData);
    }
}

[System.Serializable]
public class CustomGenericScriptableValueEvent<T>
{
    public ScriptableValue<T> gameEvent;
    public string eventName;
    public string parameterName;

    public void SendEvent(T parameters)
    {
        Dictionary<string, object> eventData = new Dictionary<string, object>();
        eventData.Add(parameterName, parameters);
        Analytics.CustomEvent(eventName, eventData);
    }
}
