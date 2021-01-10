using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PerfectShieldDisplay : MonoBehaviour
{
    public GameObject PerfectShieldObject;
    public BoolValue IsPerfectShielding;

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
        PerfectShieldObject.SetActive(toggle);
    }
}
