using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectile : Projectile, IRevertableProjectile
{
    public float revertSpeedBuff = 3;

    private void Update()
    {
        transform.Translate(direction * horizontalSpeed * deltaTime.Value);
    }

    public void OnRevert()
    {
        direction = new Vector2(direction.x * -1, direction.y);
        horizontalSpeed *= revertSpeedBuff;
        GetComponent<DamageDealer>().currentOrigin = DamageOrigin.allied;
    }
}
