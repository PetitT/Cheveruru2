﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public FloatValue DeltaTime;
    public FloatValue TimeScale;

    public FloatGameEvent FreezeFrameRequest;

    private bool isFreezingFrame = false;

    private void OnEnable()
    {
        FreezeFrameRequest.onEventRaised += OnFreezeFrameRequest;
    }

    private void OnDisable()
    {
        FreezeFrameRequest.onEventRaised -= OnFreezeFrameRequest;
    }

    public void OnFreezeFrameRequest(float obj)
    {
        if (!isFreezingFrame)
        {
            StartCoroutine(FreezeFrame(obj));
        }
    }

    private void Update()
    {
        DeltaTime.RuntimeValue = Time.deltaTime * TimeScale.RuntimeValue;
    }

    private IEnumerator FreezeFrame(float time)
    {
        isFreezingFrame = true;
        TimeScale.RuntimeValue = 0;
        yield return new WaitForSeconds(time);
        TimeScale.RuntimeValue = TimeScale.InitialValue;
        isFreezingFrame = false;
    }
}
