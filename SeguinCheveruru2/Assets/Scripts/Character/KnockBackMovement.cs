﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackMovement : MovementEffect
{
    public KnockBackGameEvent KnockBackEvent;
    public FloatValue KnockBackDeceleration;
    public FloatValue DeltaTime;

    private Vector2 currentDirection;
    private float currentForce;

    private void Awake()
    {
        KnockBackEvent.onEventRaised += KnockBackHandler;
    }

    private void OnDestroy()
    {
        KnockBackEvent.onEventRaised -= KnockBackHandler;
    }

    private void KnockBackHandler(KnockBackData obj)
    {
        currentDirection = obj.direction;
        currentForce = obj.force;
    }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        currentForce -= KnockBackDeceleration.RuntimeValue * DeltaTime.RuntimeValue;
        currentForce = Mathf.Max(currentForce, 0);
        pMovement = currentDirection * currentForce * DeltaTime.RuntimeValue;
    }
}
