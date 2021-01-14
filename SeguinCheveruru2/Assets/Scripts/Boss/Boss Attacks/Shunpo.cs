using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shunpo : BossAttack
{
    public float preWaitTime;
    public float invisibleTime;
    public float attackHeight;
    public float attackWait;
    public float fallSpeed;
    public float postWaitTime;
    public FloatValue deltaTime;

    public GameObject hitBox;
    public GameObject smokeBomb;

    public AudioClip smokeBombClip;
    public AudioClip reappearClip;
    public AudioClip fallClip;

    private SpriteRenderer sprite;

    protected override void Awake()
    {
        base.Awake();
        sprite = GetComponent<SpriteRenderer>();
    }

    public override IEnumerator Attack(Action onFinish)
    {
        float defaultY = transform.position.y;
        yield return new WaitForSeconds(preWaitTime);
        audioSrc.PlayOneShot(smokeBombClip);
        GameObject smoke = Pool.Instance.GetItemFromPool(smokeBomb, transform.position, Quaternion.identity);
        sprite.enabled = false;
        transform.position = new Vector2(22, 100);
        yield return new WaitForSeconds(invisibleTime);
        Vector2 playerPosition = BossDirection.Instance.target.position;
        Vector2 attackPosition = playerPosition.ModifyY(playerPosition.y + attackHeight);
        transform.position = attackPosition;
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.AirIdle);
        audioSrc.PlayOneShot(reappearClip);
        sprite.enabled = true;
        GameObject newSmoke = Pool.Instance.GetItemFromPool(smokeBomb, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(attackWait);
        audioSrc.PlayOneShot(fallClip);
        hitBox.SetActive(true);
        while (transform.position.y > defaultY)
        {
            transform.Translate(Vector2.down * fallSpeed * deltaTime.Value);
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, defaultY);
        hitBox.SetActive(false);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.StandingIdle);
        yield return new WaitForSeconds(postWaitTime);
        onFinish?.Invoke();
    }

    public override void CancelAttack()
    {
        StopCoroutine(Attack(() => { }));
    }
}
