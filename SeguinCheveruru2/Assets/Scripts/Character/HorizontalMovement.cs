using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : Move
{
    public FloatValue MovementSpeed;
    public FloatValue ShieldingMoveSpeed;
    public FloatValue AttackingMoveSpeed;
    public FloatValue Acceleration;
    public FloatValue XValue;
    public FloatValue DeltaTime;
    public BoolValue IsShielding;
    public BoolValue IsAttacking;
    public BoolValue IsJumping;
    public GameObject body;

    private float currentSpeed;

    private Vector2 lastDirection = Vector2.zero;

    public void Move(float X)
    {
        XValue.Value = X;
        Vector2 direction = lastDirection;

        if (X > 0)
        {
            if (!IsAttacking.Value)
            {
                body.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            direction = Vector2.right;
        }
        else if (X < 0)
        {
            if (!IsAttacking.Value)
            {
                body.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            direction = Vector2.left;
        }

        AdaptSpeed();
        ApplySpeed(direction);
    }

    private void AdaptSpeed()
    {
        float targetSpeed;

        if (IsShielding.Value)
        {
            targetSpeed = ShieldingMoveSpeed.Value;
        }
        else if (IsAttacking.Value && !IsJumping.Value)
        {
            targetSpeed = AttackingMoveSpeed.Value;
        }
        else
        {
            targetSpeed = MovementSpeed.Value;
        }

        if (XValue.Value == 0)
        {
            targetSpeed = 0;
        }
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, Acceleration.Value * DeltaTime.Value);
        currentSpeed = Mathf.Max(0, currentSpeed);
    }

    private void ApplySpeed(Vector2 direction)
    {
        pMovement = direction * currentSpeed * DeltaTime.Value;
        lastDirection = direction;
    }
}
