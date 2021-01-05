using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FangThrow : BossAttack
{
    public GameObject projectile;
    public Transform throwPosition;
    public Transform leftWall;
    public Transform rightWall;

    public float moveSpeed;
    public float distanceFromWall;
    public float preWaitTime;
    public float postWaitTime;
    public Vector2 timeBetweenThrows;
    public Vector2 numberOfThrows;

    public Vector2 projectileSpeed;
    private Vector2 throwDirection;

    public override IEnumerator Attack(Action onFinish)
    {
        yield return GoToWall();
        yield return ThrowFangs();
        onFinish?.Invoke();
    }

    private IEnumerator GoToWall()
    {
        Vector2 targetPos = BossDirection.Instance.target.position.x > transform.position.x ? leftWall.position : rightWall.position;
        BossDirection.Instance.ToggleRotation(false);
        transform.rotation = targetPos == (Vector2)rightWall.position ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        while (Vector2.Distance(transform.position.Grounded(), targetPos.Grounded()) > distanceFromWall)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPos.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        BossDirection.Instance.ToggleRotation(true);
    }

    private IEnumerator ThrowFangs()
    {
        yield return new WaitForSeconds(preWaitTime);
        for (int i = 0; i < numberOfThrows.RandomRange(); i++)
        {
            GameObject newProjectile = Pool.Instance.GetItemFromPool(projectile, throwPosition.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().SetProjectileData(projectileSpeed.RandomRange(), BossDirection.Instance.currentDirection);
            yield return new WaitForSeconds(timeBetweenThrows.RandomRange());
        }
        yield return new WaitForSeconds(postWaitTime);
    }
}
