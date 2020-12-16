using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirection : Singleton<BossDirection>
{
    [SerializeField] private Transform target;
    [SerializeField] private SpriteRenderer sprite;
    public BoolValue isAttacking;

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
        if (!isAttacking.Value)
        {
            if (GetDirection() == Vector2.right)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
    }


}
