using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public FloatValue deltaTime;
    public FloatValue bossHealth;
    public BoolValue isAttacking;

    public List<BossPhase> phases;
    private int currentPhaseIndex = 0;
    private BossPhase currentPhase => phases[currentPhaseIndex];
    private BossAttack currentAttack = null;
    private BossAttack lastAttack = null;
    private float remainingTimeToAttack;

    private bool canAttack = true;

    private void Awake()
    {
        remainingTimeToAttack = currentPhase.timeBetweenAttacks.RandomRange();
        bossHealth.onValueChanged += BossHealthValueChangedHandler;
    }

    private void OnDestroy()
    {
        bossHealth.onValueChanged -= BossHealthValueChangedHandler;
    }


    private void Update()
    {
        if (canAttack)
        {
            CheckAttack();
        }
    }

    private void BossHealthValueChangedHandler(float obj)
    {
        if(obj <= 0)
        {
            canAttack = false;
            currentAttack?.CancelAttack();
        }
        if (currentPhaseIndex >= phases.Count) { return; }
        if (bossHealth.Value / bossHealth.InitialValue < currentPhase.threshold)
        {
            currentPhaseIndex++;
        }
    }

    private void CheckAttack()
    {
        if (isAttacking.Value) { return; }
        remainingTimeToAttack -= deltaTime.Value;
        if (remainingTimeToAttack <= 0)
        {
            isAttacking.Value = true;
            remainingTimeToAttack = currentPhase.timeBetweenAttacks.RandomRange();
            currentAttack = GetRandomAttack();
            StartCoroutine(currentAttack.Attack(() => isAttacking.Value = false));
        }
    }

    private BossAttack GetRandomAttack()
    {
        BossAttack attack = currentPhase.attacks.GetRandom();
        if (lastAttack != null && lastAttack == attack)
        {
            return GetRandomAttack();
        }
        else
        {
            lastAttack = attack;
            return attack;
        }
    }
}
