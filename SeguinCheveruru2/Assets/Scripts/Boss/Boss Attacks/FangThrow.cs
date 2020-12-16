using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FangThrow : BossAttack
{
    public GameObject projectile;
    public Transform throwPosition;

    public float preWaitTime;
    public float postWaitTime;
    public Vector2 timeBetweenThrows;
    public Vector2 numberOfThrows;

    public Vector2 projectileSpeed;
    private Vector2 throwDirection;

    public override IEnumerator Attack(Action onFinish)
    {
        yield return new WaitForSeconds(preWaitTime);
        for (int i = 0; i < numberOfThrows.RandomRange(); i++)
        {
            GameObject newProjectile = Pool.Instance.GetItemFromPool(projectile, throwPosition.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().SetProjectileData(projectileSpeed.RandomRange(), BossDirection.Instance.currentDirection);
            yield return new WaitForSeconds(timeBetweenThrows.RandomRange());
        }
        yield return new WaitForSeconds(postWaitTime);
        onFinish?.Invoke();
    }
}
