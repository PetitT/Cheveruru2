using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleClawAttack : DoubleClawAttack
{
    public float minDistanceToPlayerThree;
    public float windupTimeThree;
    public float endLagTimeThree;
    public GameObject hitBoxThree;
    public AudioClip clipThree;
    public float dashSpeed;
    public FloatValue deltaTime;
    private bool isDashing;

    public override IEnumerator Attack(Action onFinish)
    {
        yield return GoToPlayer(minDistanceToPlayerOne);
        yield return DoWindup(BossAnimation.BossAnim.WindupOne, windupTime);
        yield return DoClawAttack(hitBox, endLagTime, clipOne);

        yield return GoToPlayer(minDistanceToPlayerTwo);
        yield return DoWindup(BossAnimation.BossAnim.WindupTwo, windupTimeTwo);
        yield return DoClawAttack(hitBoxTwo, endLagTimeTwo, clipTwo);

        yield return GoToPlayer(minDistanceToPlayerThree);
        yield return DoWindup(BossAnimation.BossAnim.WindupThree, windupTimeThree);
        isDashing = true;
        yield return DoClawAttack(hitBoxThree, endLagTimeThree, clipThree);
        isDashing = false;

        onFinish?.Invoke();
    }

    private void Update()
    {
        if (isDashing)
        {
            Dash();
        }
    }

    private void Dash()
    {
        Vector2 direction = transform.rotation == Quaternion.Euler(0, 0, 0) ? Vector2.left : Vector2.right;
        transform.position += (Vector3)direction * dashSpeed * deltaTime.Value;
    }
}
