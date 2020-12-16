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
    public KnockBackGameEvent KnockBackRequest;

    private void Awake()
    {
        IsShielding.onValueChanged += ShieldHandler;
        OnPerfectParry.onEventRaised += PerfectShieldHandler;
        IsAirborne.onValueChanged += AirborneValueChangedHandler;
        KnockBackRequest.onEventRaised += KnockBackRequestHandler;
    }

    private void OnDestroy()
    {
        IsShielding.onValueChanged -= ShieldHandler;
        OnPerfectParry.onEventRaised -= PerfectShieldHandler;
        IsAirborne.onValueChanged -= AirborneValueChangedHandler;
        KnockBackRequest.onEventRaised -= KnockBackRequestHandler;
    }

    private void ShieldHandler(bool obj)
    {
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
