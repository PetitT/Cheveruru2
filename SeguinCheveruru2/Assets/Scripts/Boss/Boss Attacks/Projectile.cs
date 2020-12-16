using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public FloatValue deltaTime;
    protected float horizontalSpeed;
    protected Vector2 direction;

    public void SetProjectileData(float speed, Vector2 direction)
    {
        this.horizontalSpeed = speed;
        this.direction = direction;
    }
}
