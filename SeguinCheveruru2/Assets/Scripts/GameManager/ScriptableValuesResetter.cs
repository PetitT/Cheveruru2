using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableValuesResetter : MonoBehaviour
{
    public List<ScriptableValue<float>> floats;
    public List<ScriptableValue<int>> ints;
    public List<ScriptableValue<bool>> bools;

    private void Awake()
    {
        ResetValues(floats);
        ResetValues(ints);
        ResetValues(bools);
    }

    private void ResetValues<T>(List<ScriptableValue<T>> values)
    {
        values.ForEach(t => t.Value = t.InitialValue);
    }
}
