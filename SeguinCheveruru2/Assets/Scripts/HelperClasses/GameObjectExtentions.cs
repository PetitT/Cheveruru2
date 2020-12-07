using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtentions
{
    public static bool TryGetComponentInParent<T>(this Component comp, out T component)
    {
        component = comp.GetComponentInParent<T>();
        return HasFoundComponent(component);
    }
    public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T component)
    {
        component = gameObject.GetComponentInParent<T>();
        return HasFoundComponent(component);
    }

    public static bool TryGetComponentInChildren<T>(this Component comp, out T component)
    {
        component = comp.GetComponentInChildren<T>();
        return HasFoundComponent(component);
    }

    public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component)
    {
        component = gameObject.GetComponentInChildren<T>();
        return HasFoundComponent(component);
    }

    public static bool TryGetComponentsInParent<T>(this Component comp, out T[] components)
    {
        components = comp.GetComponentsInParent<T>();
        return HasFoundComponent(components);
    }

    public static bool TryGetComponentsInParent<T>(this GameObject gameObject, out T[] components)
    {
        components = gameObject.GetComponentsInParent<T>();
        return HasFoundComponent(components);
    }

    public static bool TryGetComponentsInChildren<T>(this Component comp, out T[] components)
    {
        components = comp.GetComponentsInChildren<T>();
        return HasFoundComponents(components);
    }

    public static bool TryGetComponentsInChildren<T>(this GameObject gameObject, out T[] components)
    {
        components = gameObject.GetComponentsInChildren<T>();
        return HasFoundComponents(components);
    }

    private static bool HasFoundComponent<T>(T component)
    {
        if (component == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static bool HasFoundComponents<T>(T[] components)
    {
        if (components == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
