using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MovementEffect
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
    public GameObject Body;

    private float currentSpeed;

    private Vector2 lastDirection = Vector2.zero;

    public void Move(float X)
    {
        XValue.RuntimeValue = X;
        Vector2 direction = lastDirection;

        if (X > 0)
        {
            if (!IsAttacking.RuntimeValue)
            {
                Body.transform.localRotation = Quaternion.identity;
            }
            direction = Vector2.right;
        }
        else if (X < 0)
        {
            if (!IsAttacking.RuntimeValue)
            {
                Body.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            direction = Vector2.left;
        }

        AdaptSpeed();
        ApplySpeed(direction);
    }

    private void AdaptSpeed()
    {
        float targetSpeed;

        if (IsShielding.RuntimeValue)
        {
            targetSpeed = ShieldingMoveSpeed.RuntimeValue;
        }
        else if (IsAttacking.RuntimeValue && !IsJumping.RuntimeValue)
        {
            targetSpeed = AttackingMoveSpeed.RuntimeValue;
        }
        else
        {
            targetSpeed = MovementSpeed.RuntimeValue;
        }

        if (XValue.RuntimeValue == 0)
        {
            targetSpeed = 0;
        }
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, Acceleration.RuntimeValue * DeltaTime.RuntimeValue);
        currentSpeed = Mathf.Max(0, currentSpeed);
    }

    private void ApplySpeed(Vector2 direction)
    {
        pMovement = direction * currentSpeed * DeltaTime.RuntimeValue;
        lastDirection = direction;
    }
}
