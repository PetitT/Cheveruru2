using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGameEvent<T> : ScriptableObject
{
    public event Action<T> onEventRaised;

    public void Raise(T obj)
    {
        onEventRaised?.Invoke(obj);
    }
}
