using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClawAttack : ClawAttack
{
    public float windupTimeTwo;
    public float endLagTimeTwo;
    public GameObject hitBoxTwo;

    public override IEnumerator Attack(Action onFinish)
    {
        if (Vector2.Distance(transform.position.Grounded(), BossDirection.Instance.target.position.Grounded()) > minDistanceToPlayer)
        {
            yield return GoToPlayer();
        }
        yield return DoWindup(BossAnimation.BossAnim.WindupOne, windupTime);
        yield return DoClawAttack(hitBox, endLagTime);

        yield return GoToPlayer();

        yield return DoWindup(BossAnimation.BossAnim.WindupTwo, windupTimeTwo);
        yield return DoClawAttack(hitBoxTwo, endLagTimeTwo);
        onFinish?.Invoke();
    }
}