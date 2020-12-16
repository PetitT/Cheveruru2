using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public enum RotationMode { local, world }
    public FloatValue deltaTime;

    public bool rotateOnXAxis;
    public RotationMode XRotationMode = RotationMode.world;
    public float XSpeed;
    public bool rotateOnYAxis;
    public RotationMode YRotationMode = RotationMode.world;
    public float YSpeed;
    public bool rotateOnZAxis;
    public RotationMode ZRotationMode = RotationMode.world;
    public float ZSpeed;

    private void Update()
    {
        if (rotateOnXAxis)
        {
            Vector3 axis = XRotationMode == RotationMode.local ? transform.right : Vector3.right;
            Rotate(axis, XSpeed);
        }
        if (rotateOnYAxis)
        {
            Vector3 axis = YRotationMode == RotationMode.local ? transform.up : Vector3.up;
            Rotate(axis, YSpeed);
        }
        if (rotateOnZAxis)
        {
            Vector3 axis = ZRotationMode == RotationMode.local ? transform.forward : Vector3.forward;
            Rotate(axis, ZSpeed);
        }
    }

    private void Rotate(Vector3 axis, float speed)
    {
        gameObject.transform.Rotate(axis * speed * deltaTime.Value);
    }
}
