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

    private float remainingTimeToAttack;

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
        CheckAttack();
    }

    private void BossHealthValueChangedHandler(float obj)
    {
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
            StartCoroutine(currentPhase.attacks.GetRandom().Attack(() => isAttacking.Value = false));
        }
    }
}
