using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shunpo : BossAttack
{
    public float preWaitTime;
    public float attackHeight;
    public float attackWait;
    public float fallSpeed;
    public float postWaitTime;
    public FloatValue deltaTime;

    public GameObject hitBox;

    public override IEnumerator Attack(Action onFinish)
    {
        float defaultY = transform.position.y;
        yield return new WaitForSeconds(preWaitTime);
        Vector2 playerPosition = BossDirection.Instance.target.position;
        Vector2 attackPosition = playerPosition.ModifyY(playerPosition.y + attackHeight);
        transform.position = attackPosition;
        yield return new WaitForSeconds(attackWait);
        hitBox.SetActive(true);
        while(transform.position.y > defaultY)
        {
            transform.Translate(Vector2.down * fallSpeed * deltaTime.Value);
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, defaultY);
        hitBox.SetActive(false);
        yield return new WaitForSeconds(postWaitTime);
        onFinish?.Invoke();
    }
}
