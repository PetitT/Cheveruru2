using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class JumpMovement : MovementEffect
{
    public FloatValue JumpForce;
    public FloatValue IncreasedJumpForcePerSec;
    public FloatValue IncreaseJumpTime;
    public FloatValue DeltaTime;
    public FloatValue RisingGravity;
    public FloatValue FallingGravity;
    public BoolValue isAirborne;
    public IntValue jumpAmount;
    public LayerValue groundLayer;
    public BoolValue IsAttacking;
    public Transform groundCheck;
    public float distanceFromGround;

    public FloatValue Yvelocity;
    public float terminalVelocity;

    private bool isJumping = false;

    private void Start()
    {
        Yvelocity.RuntimeValue = 0;
    }

    public void Jump()
    {
        if (IsAttacking.RuntimeValue && !isAirborne.RuntimeValue) { return; }
        if (jumpAmount.RuntimeValue > 0)
        {
            isAirborne.RuntimeValue = true;
            isJumping = true;
            IncreaseJumpTime.RuntimeValue = IncreaseJumpTime.InitialValue;
            Yvelocity.RuntimeValue = JumpForce.RuntimeValue;
            jumpAmount.RuntimeValue--;
        }
    }

    public void ReleaseJump()
    {
        isJumping = false;
    }

    private void Update()
    {
        if (isAirborne)
        {
            ApplyGravity();
        }
        if (Yvelocity.RuntimeValue <= 0)
        {
            CheckGround();
        }
        if (isJumping)
        {
            IncreaseJump();
        }

        ApplyForce();
    }

    private void IncreaseJump()
    {
        Yvelocity.RuntimeValue += IncreasedJumpForcePerSec.RuntimeValue * DeltaTime.RuntimeValue;
        IncreaseJumpTime.RuntimeValue -= DeltaTime.RuntimeValue;
        if (IncreaseJumpTime.RuntimeValue <= 0)
        {
            isJumping = false;
        }
    }

    private void CheckGround()
    {
        if (Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 2f), new Vector2(transform.position.x + 0.5f, transform.position.y - 2.01f), groundLayer.RuntimeValue) != null)
        {
            if (isAirborne)
            {
                isAirborne.RuntimeValue = false;
                Yvelocity.RuntimeValue = 0;
                jumpAmount.RuntimeValue = jumpAmount.InitialValue;
            }
        }
        else
        {
            if (!isAirborne)
            {
                jumpAmount.RuntimeValue--;
                isAirborne.RuntimeValue = true;
            }
        }
    }

    private void ApplyGravity()
    {
        Yvelocity.RuntimeValue -= (Yvelocity.RuntimeValue > 0 ? RisingGravity.RuntimeValue : FallingGravity.RuntimeValue) * DeltaTime.RuntimeValue;
        Yvelocity.RuntimeValue = Mathf.Max(Yvelocity.RuntimeValue, terminalVelocity);
    }

    private void ApplyForce()
    {
        pMovement = Yvelocity.RuntimeValue * Vector2.up * DeltaTime.RuntimeValue;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 2f), new Vector2(1, 0.01f));
    }

}
