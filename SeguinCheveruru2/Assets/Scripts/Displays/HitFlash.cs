using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    public SpriteRenderer hitFlashSprite;
    public GameEvent hitFlashRequest;
    public float flashTime;
    public float blendValue;

    private void Awake()
    {
        hitFlashRequest.onEventRaised += Flash;
    }

    private void OnDestroy()
    {
        hitFlashRequest.onEventRaised -= Flash;
    }

    private void Flash()
    {
        StopCoroutine(DoFlash());
        StartCoroutine(DoFlash());
    }

    private IEnumerator DoFlash()
    {
        hitFlashSprite.material.SetFloat("_HitEffectBlend", blendValue);
        yield return new WaitForSeconds(flashTime);
        hitFlashSprite.material.SetFloat("_HitEffectBlend", 0);
    }
}
