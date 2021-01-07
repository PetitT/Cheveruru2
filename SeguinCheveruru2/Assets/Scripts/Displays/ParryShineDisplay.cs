using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryShineDisplay : MonoBehaviour
{
    public GameEvent onShield;
    public GameEvent onPerfectShield;

    public Color shieldColor;
    public Color perfectShieldColor;

    public float shineSpeed;

    public SpriteRenderer charSprite;

    public string ShineColor;
    public string ShineLocation;

    private void Awake()
    {
        charSprite.material.SetFloat(ShineLocation, -1f);
        onShield.onEventRaised += ShieldDisplay;
        onPerfectShield.onEventRaised += PerfectShieldDisplay;
    }

    private void OnDestroy()
    {
        onShield.onEventRaised -= ShieldDisplay;
        onPerfectShield.onEventRaised -= PerfectShieldDisplay;
    }

    private void PerfectShieldDisplay()
    {
        charSprite.material.SetColor(ShineColor, perfectShieldColor);
        StopCoroutine(Shine());
        StartCoroutine(Shine());
    }

    private void ShieldDisplay()
    {
        charSprite.material.SetColor(ShineColor, shieldColor);
        StopCoroutine(Shine());
        StartCoroutine(Shine());
    }

    public IEnumerator Shine()
    {
        float currentLocation = 0.35f;
        while (currentLocation < 1)
        {
            currentLocation += shineSpeed * Time.deltaTime;
            charSprite.material.SetFloat(ShineLocation, currentLocation);
            yield return null;
        }
        charSprite.material.SetFloat(ShineLocation, -1f);
    }
}
