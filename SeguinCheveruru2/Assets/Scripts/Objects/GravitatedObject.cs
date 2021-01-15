using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitatedObject : MonoBehaviour
{
    public float YgroundPosition;
    public float fallAcceleration;

    private float fallSpeed;

    private void Update()
    {
        Fall();
        Accelerate();
    }

    private void Accelerate()
    {
        fallSpeed += Mathf.Sqrt(fallAcceleration * Time.deltaTime);
    }

    private void Fall()
    {
        if (transform.position.y > YgroundPosition)
        {
            gameObject.transform.Translate(Vector2.down * fallSpeed);
        }
    }
}
