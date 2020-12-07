using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        SetAsSingleton();
    }

    private void SetAsSingleton()
    {
        instance = this as T;
    }
}
