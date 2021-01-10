using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarDisplay : MonoBehaviour
{
    public Image shieldDisplay;
    public GameEvent onBrokenShield;
    public GameEvent onShieldRegained;
    public FloatValue shieldAmount;

    public Color brokenShieldColor;
    public Color availableShieldColor;

    private float maxHeight;

    private void Awake()
    {
        onBrokenShield.onEventRaised += BrokenShieldHandler;
        onShieldRegained.onEventRaised += ShieldRegainedHandler;
        shieldAmount.onValueChanged += ShieldAmountDisplay;

        shieldDisplay.color = availableShieldColor;
        maxHeight = shieldDisplay.rectTransform.rect.height;
    }

    private void OnDestroy()
    {
        onBrokenShield.onEventRaised -= BrokenShieldHandler;
        onShieldRegained.onEventRaised -= ShieldRegainedHandler;
        shieldAmount.onValueChanged -= ShieldAmountDisplay;
    }

    private void ShieldAmountDisplay(float amount)
    {
        float normalizedShield = amount / shieldAmount.InitialValue;
        float newHeight = maxHeight * normalizedShield;
        shieldDisplay.rectTransform.sizeDelta = new Vector2(shieldDisplay.rectTransform.sizeDelta.x, newHeight); 
    }

    private void BrokenShieldHandler()
    {
        shieldDisplay.color = brokenShieldColor;
    }

    private void ShieldRegainedHandler()
    {
        shieldDisplay.color = availableShieldColor;
    }
}
