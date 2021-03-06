﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableValue<T> : ScriptableObject, ISerializationCallbackReceiver
{
    public T InitialValue;
    [SerializeField]
    private T runtimeValue;

    public T Value
    {
        get
        {
            return runtimeValue;
        }
        set
        {
            runtimeValue = value;
            onValueChanged?.Invoke(this.Value);
        }
    }

    public event Action<T> onValueChanged;

    public void OnAfterDeserialize()
    {
        Value = InitialValue;
    }

    public void OnBeforeSerialize() { }
}
