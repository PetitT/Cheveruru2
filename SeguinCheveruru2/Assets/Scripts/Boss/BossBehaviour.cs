using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public FloatValue deltaTime;
    public List<BossPhase> phases;
    private int currentPhaseIndex = 0;
    private BossPhase currentPhase => phases[currentPhaseIndex];

    private BoolValue canAttack;
    private float remainingTimeToAttack;

    private void Awake()
    {
        remainingTimeToAttack = currentPhase.timeBetweenAttacks.Value.RandomRange();
    }

    private void Update()
    {
        CheckAttack();
    }

    private void CheckAttack()
    {
        if (!canAttack.Value) { return; }
        remainingTimeToAttack -= deltaTime.Value;
        if(remainingTimeToAttack <= 0)
        {
            canAttack.Value = false;
            remainingTimeToAttack = currentPhase.timeBetweenAttacks.Value.RandomRange();
            StartCoroutine(currentPhase.attacks.GetRandom().Attack(() => canAttack.Value = true));
        }
    }
}
