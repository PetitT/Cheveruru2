using System;
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
        yield return GoToPlayer(minDistanceToPlayerOne);
        yield return DoWindup(BossAnimation.BossAnim.WindupOne, windupTime);
        yield return DoClawAttack(hitBox, endLagTime, clipOne);

        yield return GoToPlayer(minDistanceToPlayerTwo);
        yield return DoWindup(BossAnimation.BossAnim.WindupTwo, windupTimeTwo);
        yield return DoClawAttack(hitBoxTwo, endLagTimeTwo, clipTwo);
        onFinish?.Invoke();
    }
}