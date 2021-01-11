using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : BossAttack
{
    public float runSpeed;
    public float minDistanceToPlayerOne;
    public float windupTime;
    public float endLagTime;
    public GameObject hitBox;
    public AudioClip clipOne;
    private bool isWaitingForAnim;

    public override IEnumerator Attack(Action onFinish)
    {
        if (!IsCloseToPlayer(minDistanceToPlayerOne))
        {
            yield return GoToPlayer(minDistanceToPlayerOne);
        }
        yield return DoWindup(BossAnimation.BossAnim.WindupOne, windupTime);
        yield return DoClawAttack(hitBox, endLagTime, clipOne);
        onFinish?.Invoke();
    }

    public IEnumerator GoToPlayer(float distance)
    {
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.StandingRun);
        while (!IsCloseToPlayer(distance))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(BossDirection.Instance.target.position.x, transform.position.y), runSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public virtual IEnumerator DoWindup(BossAnimation.BossAnim windupAnim, float windupTime)
    {
        BossDirection.Instance.ToggleRotation(false);
        BossAnimation.Instance.Animate(windupAnim);
        isWaitingForAnim = true;
        while (isWaitingForAnim)
        {
            yield return null;
        }
        BossAnimation.Instance.ToggleAnimation(false);
        yield return new WaitForSeconds(windupTime);
        BossAnimation.Instance.ToggleAnimation(true);
    }

    public virtual IEnumerator DoClawAttack(GameObject hitBoxObject, float endLag, AudioClip soundEffect)
    {
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.Attack);
        audioSrc.PlayOneShot(soundEffect);
        isWaitingForAnim = true;
        hitBoxObject.SetActive(true);
        while (isWaitingForAnim)
        {
            yield return null;
        }
        hitBoxObject.SetActive(false);
        BossAnimation.Instance.ToggleAnimation(false);
        yield return new WaitForSeconds(endLag);
        BossDirection.Instance.ToggleRotation(true);
        BossAnimation.Instance.ToggleAnimation(true);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.StandingIdle);
    }

    public void StopWait()
    {
        isWaitingForAnim = false;
    }

    protected bool IsCloseToPlayer(float distance)
    {
        return Vector2.Distance(transform.position.Grounded(), BossDirection.Instance.target.position.Grounded()) < distance;
    }
}
