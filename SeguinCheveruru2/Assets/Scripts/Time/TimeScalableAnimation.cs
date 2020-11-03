using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TimeScalableAnimation : MonoBehaviour
{
    public FloatValue TimeScale;
    private Animator anim;

    private void Awake()
    {
        TimeScale.onValueChanged += ValueChangedHandler;
        anim = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        TimeScale.onValueChanged -= ValueChangedHandler;
    }

    private void ValueChangedHandler(float obj)
    {
        anim.speed = obj;
    }
}
