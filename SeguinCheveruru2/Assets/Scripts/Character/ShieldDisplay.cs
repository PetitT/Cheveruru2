using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ShieldDisplay : MonoBehaviour
{
    public FloatValue ShieldValue;
    public BoolValue IsShieldingValue;
    public SpriteRenderer ShieldObject;

    private void Awake()
    {
        ShieldValue.onValueChanged += ShieldDisplayHandler;
        IsShieldingValue.onValueChanged += IsShieldingHandler;
    }
    private void OnDestroy()
    {
        ShieldValue.onValueChanged -= ShieldDisplayHandler;
        IsShieldingValue.onValueChanged -= IsShieldingHandler;
    }

    private void IsShieldingHandler(bool obj)
    {
        ShieldObject.enabled = obj;
    }

    private void ShieldDisplayHandler(float obj)
    {
        float normalizedShield = obj / ShieldValue.InitialValue;

        ShieldObject.color = new Color(ShieldObject.color.r, ShieldObject.color.g, ShieldObject.color.b, normalizedShield);
    }
}
