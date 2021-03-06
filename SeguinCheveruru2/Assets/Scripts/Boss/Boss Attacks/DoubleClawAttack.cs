﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClawAttack : ClawAttack
{
    public float minDistanceToPlayerTwo;
    public float windupTimeTwo;
    public float endLagTimeTwo;
    public GameObject hitBoxTwo;
    public AudioClip clipTwo;

    public override IEnumerator Attack(Action onFinish)
    {
        if (!IsCloseToPlayer(minDistanceToPlayerOne))
        {
            yield return GoToPlayer(minDistanceToPlayerOne);
        }
        yield return DoWindup(BossAnimation.BossAnim.WindupOne, windupTime);
        yield return DoClawAttack(hitBox, endLagTime, clipOne);

        if (!IsCloseToPlayer(minDistanceToPlayerTwo))
        {
            yield return GoToPlayer(minDistanceToPlayerTwo);
        }
        BossDirection.Instance.ToggleRotation(true);
        yield return null;
        BossDirection.Instance.ToggleRotation(false);
        yield return DoWindup(BossAnimation.BossAnim.WindupTwo, windupTimeTwo);
        yield return DoClawAttack(hitBoxTwo, endLagTimeTwo, clipTwo);
        onFinish?.Invoke();
    }

    public override void CancelAttack()
    {
        StopCoroutine(Attack(() => { }));
    }
}