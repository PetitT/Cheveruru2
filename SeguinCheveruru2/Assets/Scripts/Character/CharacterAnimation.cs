using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public FloatValue XMovement;
    public BoolValue IsAirborne;
    public Animator Anim;
    public BoolValue IsShielding;
    public GameEvent OnPerfectParry;
    public GameEvent OnShieldBreak;
    public KnockBackGameEvent KnockBackRequest;

    private bool isShieldBroken;

    private void Awake()
    {
        IsShielding.onValueChanged += ShieldHandler;
        OnPerfectParry.onEventRaised += PerfectShieldHandler;
        IsAirborne.onValueChanged += AirborneValueChangedHandler;
        KnockBackRequest.onEventRaised += KnockBackRequestHandler;
        OnShieldBreak.onEventRaised += ShieldBreakEvent;
    }

    private void OnDestroy()
    {
        IsShielding.onValueChanged -= ShieldHandler;
        OnPerfectParry.onEventRaised -= PerfectShieldHandler;
        IsAirborne.onValueChanged -= AirborneValueChangedHandler;
        KnockBackRequest.onEventRaised -= KnockBackRequestHandler;
        OnShieldBreak.onEventRaised -= ShieldBreakEvent;
    }

    private void ShieldHandler(bool obj)
    {
        if (isShieldBroken) { return; }
        if (obj)
        {
            Anim.SetTrigger("Parry");
        }
        else
        {
            Anim.SetTrigger("CancelParry");
        }
    }

    private void PerfectShieldHandler()
    {
        Anim.SetTrigger("PerfectParry");
    }

    private void ShieldBreakEvent()
    {
        Anim.SetTrigger("ShieldBreak");
        isShieldBroken = true;
        Invoke("RegenerateShield", 1f);
    }

    private void RegenerateShield()
    {
        isShieldBroken = false;
    }

    private void KnockBackRequestHandler(KnockBackData obj)
    {
        if (!IsShielding.Value)
        {
            Anim.SetTrigger("Damaged");
        }
    }

    private void Update()
    {
        if (!Anim.GetBool("IsMoving"))
        {
            if (XMovement.Value != 0)
            {
                Anim.SetBool("IsMoving", true);
            }
        }
        else
        {
            if (XMovement.Value == 0)
            {
                Anim.SetBool("IsMoving", false);
            }
        }
    }

    private void AirborneValueChangedHandler(bool obj)
    {
        if (obj)
        {
            Anim.SetBool("IsAirborne", true);
        }
        else
        {
            Anim.SetBool("IsAirborne", false);
        }
    }
}
