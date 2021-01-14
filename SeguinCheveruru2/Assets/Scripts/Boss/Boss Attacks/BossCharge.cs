using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharge : BossAttack
{
    public GameObject hitBox;

    public AudioClip preCharge;
    public AudioClip charge;

    public Transform leftTarget;
    public Transform rightTarget;

    public FloatValue deltaTime;
    public float preWaitTime;
    public float chargeSpeed;
    public float postWaitTime;

    private float securityDistance = 0.5f;

    public override IEnumerator Attack(Action onFinish)
    {
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.GroundIdle);
        audioSrc.PlayOneShot(preCharge);
        yield return new WaitForSeconds(preWaitTime);
        hitBox.SetActive(true);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.Run);
        BossDirection.Instance.ToggleRotation(false);
        Vector2 direction = BossDirection.Instance.currentDirection;
        Vector2 target = direction == Vector2.right ? rightTarget.position : leftTarget.position;
        target = new Vector2(target.x, transform.position.y);
        audioSrc.PlayOneShot(charge);
        float distance = Mathf.Abs(transform.position.x - target.x);
        while (distance > securityDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, chargeSpeed * deltaTime.Value);
            distance = Mathf.Abs(transform.position.x - target.x);
            yield return null;
        }
        hitBox.SetActive(false);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.GroundIdle);
        yield return new WaitForSeconds(postWaitTime);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.StandingIdle);
        BossDirection.Instance.ToggleRotation(true);
        onFinish?.Invoke();
    }

    public override void CancelAttack()
    {
        StopCoroutine(Attack(() => { }));
    }
}
