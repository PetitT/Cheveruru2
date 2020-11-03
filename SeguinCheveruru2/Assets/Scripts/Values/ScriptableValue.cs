using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableValue<T> : ScriptableObject, ISerializationCallbackReceiver
{
    public T InitialValue;
    [SerializeField]
    private T runtimeValue;

    public T RuntimeValue
    {
        get
        {
            return runtimeValue;
        }
        set
        {
            runtimeValue = value;
            onValueChanged?.Invoke(RuntimeValue);
        }
    }

    public event Action<T> onValueChanged;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize() { }
}
