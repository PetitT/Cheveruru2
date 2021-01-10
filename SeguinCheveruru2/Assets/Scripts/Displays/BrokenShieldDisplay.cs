using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenShieldDisplay : MonoBehaviour
{
    public ParticleSystem brokenShieldParticles;
    public ParticleSystem blueShieldParticles;
    public GameObject brokenShield;
    public Animator anim;

    public GameEvent shieldBreakEvent;
    public GameEvent shieldRegainedEvent;
    public FloatValue shieldValue;

    private float initialAnimationSpeed = 5f;
    private bool canDisplayShield = false;
    private bool wasShieldBroken = false;

    private void Awake()
    {
        shieldBreakEvent.onEventRaised += DisplayShieldBreak;
        shieldRegainedEvent.onEventRaised += DisplayShieldRegained;
    }

    private void OnDestroy()
    {
        shieldBreakEvent.onEventRaised -= DisplayShieldBreak;
        shieldRegainedEvent.onEventRaised -= DisplayShieldRegained;
    }

    private void Update()
    {
        DisplayAnimation();
    }

    private void DisplayAnimation()
    {
        if (!anim.gameObject.activeSelf || !canDisplayShield) { return; }
        float normalizedTime = shieldValue.Value / shieldValue.InitialValue;
        anim.SetFloat("normalizedTime", normalizedTime);
    }

    private void DisplayShieldBreak()
    {
        wasShieldBroken = true;
        brokenShieldParticles.Play();
        brokenShield.SetActive(true);
        StartCoroutine(BreakShieldAnim());
    }

    private void DisplayShieldRegained()
    {
        if (wasShieldBroken)
        {
            wasShieldBroken = false;
            brokenShield.SetActive(false);
            blueShieldParticles.Play();
        }
    }

    private IEnumerator BreakShieldAnim()
    {
        canDisplayShield = false;
        float normalizedTime = 1;
        while (normalizedTime > 0.1)
        {
            normalizedTime -= Time.deltaTime * initialAnimationSpeed;
            anim.SetFloat("normalizedTime", normalizedTime);
            yield return null;
        }
        canDisplayShield = true;
    }

}
