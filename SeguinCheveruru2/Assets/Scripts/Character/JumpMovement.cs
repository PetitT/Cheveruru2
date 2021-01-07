using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMovement : Move
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
    //public Transform groundCheck;
    //public float distanceFromGround;

    public FloatValue Yvelocity;
    public float terminalVelocity;

    private bool isJumping = false;

    private void Start()
    {
        Yvelocity.Value = 0;
    }

    public void Jump()
    {
        if (IsAttacking.Value && !isAirborne.Value) { return; }
        if (jumpAmount.Value > 0)
        {
            isAirborne.Value = true;
            isJumping = true;
            IncreaseJumpTime.Value = IncreaseJumpTime.InitialValue;
            Yvelocity.Value = JumpForce.Value;
            jumpAmount.Value--;
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
        if (Yvelocity.Value <= 0)
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
        Yvelocity.Value += IncreasedJumpForcePerSec.Value * DeltaTime.Value;
        IncreaseJumpTime.Value -= DeltaTime.Value;
        if (IncreaseJumpTime.Value <= 0)
        {
            isJumping = false;
        }
    }

    private void CheckGround()
    {
        if (Physics2D.OverlapArea(new Vector2(transform.position.x - 2f, transform.position.y - 4f), new Vector2(transform.position.x + 2f, transform.position.y - 4.2f), groundLayer.Value) != null)
        {
            if (isAirborne)
            {
                isAirborne.Value = false;
                Yvelocity.Value = 0;
                jumpAmount.Value = jumpAmount.InitialValue;
            }
        }
        else
        {
            if (!isAirborne)
            {
                jumpAmount.Value--;
                isAirborne.Value = true;
            }
        }
    }

    private void ApplyGravity()
    {
        Yvelocity.Value -= (Yvelocity.Value > 0 ? RisingGravity.Value : FallingGravity.Value) * DeltaTime.Value;
        Yvelocity.Value = Mathf.Max(Yvelocity.Value, terminalVelocity);
    }

    private void ApplyForce()
    {
        pMovement = Yvelocity.Value * Vector2.up * DeltaTime.Value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 2f), new Vector2(1, 0.01f));
    }

}
