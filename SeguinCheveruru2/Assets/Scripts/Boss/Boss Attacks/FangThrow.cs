﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FangThrow : BossAttack
{
    public GameObject projectile;
    public Transform throwPosition;
    public Transform leftWall;
    public Transform rightWall;
    public Transform middle;

    public GameObject hitBox;

    public float moveSpeed;
    public float distanceFromWall;
    public float preWaitTime;
    public float postWaitTime;
    public Vector2 timeBetweenThrows;
    public Vector2 numberOfThrows;

    public Vector2 projectileSpeed;
    private Vector2 throwDirection;

    public List<AudioClip> throwClips;
    private AudioClip lastClip;

    private bool isWaitingForAnim = false;

    public override IEnumerator Attack(Action onFinish)
    {
        yield return GoToWall();
        yield return ThrowFangs();
        onFinish?.Invoke();
    }

    public override void CancelAttack()
    {
        StopCoroutine(Attack(() => { }) );
    }

    private IEnumerator GoToWall()
    {
        Vector2 targetPos = FindTargetPos();
       // Vector2 targetPos = BossDirection.Instance.target.position.x > transform.position.x ? leftWall.position : rightWall.position;
        if (IsAtWall(targetPos)) { yield break; }
        transform.rotation = targetPos == (Vector2)rightWall.position ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.StandingRun);
        BossDirection.Instance.ToggleRotation(false);

        while (!IsAtWall(targetPos))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPos.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        BossDirection.Instance.ToggleRotation(true);
    }

    private IEnumerator ThrowFangs()
    {
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.WindupOne);
        isWaitingForAnim = true;
        while (isWaitingForAnim)
        {
            yield return null;
        }
        BossAnimation.Instance.ToggleAnimation(false);
        yield return new WaitForSeconds(preWaitTime);
        BossAnimation.Instance.ToggleAnimation(true);

        for (int i = 0; i < numberOfThrows.RandomRange(); i++)
        {
            BossAnimation.Instance.Animate(BossAnimation.BossAnim.Throw);
            audioSrc.PlayOneShot(GetRandomClip());
            isWaitingForAnim = true;
            hitBox.SetActive(true);
            while (isWaitingForAnim)
            {
                yield return null;
            }
            hitBox.SetActive(false);
            GameObject newProjectile = Pool.Instance.GetItemFromPool(projectile, throwPosition.position.ModifyZ(-1), Quaternion.identity);
            newProjectile.GetComponent<Projectile>().SetProjectileData(projectileSpeed.RandomRange(), BossDirection.Instance.currentDirection);
            yield return new WaitForSeconds(timeBetweenThrows.RandomRange());
        }
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.StandingIdle);
        yield return new WaitForSeconds(postWaitTime);
    }

    private bool IsAtWall(Vector2 targetPos)
    {
        return Vector2.Distance(transform.position.Grounded(), targetPos.Grounded()) < distanceFromWall;
    }

    public void StopWait()
    {
        isWaitingForAnim = false;
    }

    private AudioClip GetRandomClip()
    {
        AudioClip newClip = throwClips.GetRandom();
        if(lastClip == null || newClip != lastClip)
        {
            lastClip = newClip;
            return newClip;
        }
        else
        {
            return GetRandomClip();
        }
    }

    private Vector2 FindTargetPos()
    {
        
        float X = BossDirection.Instance.target.position.x;
        if(X > middle.position.x)
        {
            return leftWall.position;
        }
        else
        {
            return rightWall.position;
        }
    }
}
