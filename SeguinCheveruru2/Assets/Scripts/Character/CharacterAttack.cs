using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public BoolValue IsAttacking;
    public BoolValue AttackRequestAvailability;
    public BoolValue IsAirborne;
    public KnockBackGameEvent KnockBackEvent;

    public Animator anim;

    private bool wantsToAttack = false;

    private void Awake()
    {
        KnockBackEvent.onEventRaised += KnockBackHandler;
    }

    private void OnDestroy()
    {
        KnockBackEvent.onEventRaised -= KnockBackHandler;
    }

    private void KnockBackHandler(KnockBackData obj)
    {
        IsAttacking.RuntimeValue = false;
    }

    private void Update()
    {
        CheckAttackRequest();
    }

    private void CheckAttackRequest()
    {
        if (!IsAttacking.RuntimeValue && wantsToAttack)
        {
            Debug.Log("Hiku");
            wantsToAttack = false;
            IsAttacking.RuntimeValue = true;
            anim.SetTrigger("Attack");
        }
    }

    public void AttackRequest()
    {
        if (AttackRequestAvailability.RuntimeValue)
        {
            wantsToAttack = true;
        }
    }

}
