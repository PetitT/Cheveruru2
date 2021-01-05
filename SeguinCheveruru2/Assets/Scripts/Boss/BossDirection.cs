using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirection : Singleton<BossDirection>
{
    public Transform target;
    private bool canRotate = true;

    public Vector2 currentDirection => GetDirection();
    private Vector2 GetDirection()
    {
        if (target.position.x > transform.position.x)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;
        }
    }

    private void Update()
    {
        if (!canRotate) { return; }
        if (GetDirection() == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void ToggleRotation(bool toggle)
    {
        canRotate = toggle;
    }
}
