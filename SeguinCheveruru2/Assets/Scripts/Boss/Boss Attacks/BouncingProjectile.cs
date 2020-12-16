using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingProjectile : Projectile, IRevertableProjectile
{
    public AnimationCurve heightCurve;
    public float heightmultiplicator;
    public float revertSpeedBuff = 3;
    private float currentElapsedTime;
    private float defaultY = -6.08f;

    private bool wasReverted = false;

    private void OnEnable()
    {
        currentElapsedTime = 0.2f;
        wasReverted = false;
    }

    private void Update()
    {
        if (!wasReverted)
        {
            Bounce();
        }
        else
        {
            MoveForwards();
        }
    }

    private void Bounce()
    {
        currentElapsedTime += deltaTime.Value;

        float newX = gameObject.transform.position.x + (direction * horizontalSpeed * deltaTime.Value).x;
        float newY = defaultY + (heightCurve.Evaluate(currentElapsedTime % 1) * heightmultiplicator);

        gameObject.transform.position = new Vector2(newX, newY);
    }

    private void MoveForwards()
    {
        transform.Translate(direction * horizontalSpeed * deltaTime.Value);
    }

    public void OnRevert()
    {
        GetComponent<DamageDealer>().currentOrigin = DamageOrigin.allied;
        direction = (BossDirection.Instance.transform.position - transform.position).normalized;
        horizontalSpeed *= revertSpeedBuff;
    }
}
