using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameEvent/Untyped")]
public class GameEvent : ScriptableObject
{
    public event Action onEventRaised;

    public void Raise()
    {
        onEventRaised?.Invoke();
    }
}
