using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PerfectShieldDisplay : MonoBehaviour
{
    public GameObject PerfectShieldObject;
    public BoolValue IsPerfectShielding;
    public FloatValue ShieldValue;

    private void Awake()
    {
        IsPerfectShielding.onValueChanged += ValueChangedHandler;
    }

    private void OnDestroy()
    {
        IsPerfectShielding.onValueChanged -= ValueChangedHandler;
    }

    private void ValueChangedHandler(bool toggle)
    {
        if (ShieldValue.Value > 0.1f)
        {
            PerfectShieldObject.SetActive(toggle);
        }
    }
}
