using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafThrowDisplay : MonoBehaviour
{
    public Transform leavesOrigin;
    public BoolValue isJumping;
    public GameObject leaf;

    private bool lastValue = false;

    private void Awake()
    {
        isJumping.onValueChanged += ThrowLeaves;
    }

    private void ThrowLeaves(bool obj)
    {
        if (obj != lastValue)
        {
            GameObject newLeaves = Pool.Instance.GetItemFromPool(leaf, leavesOrigin.position, Quaternion.identity);
            lastValue = obj;
        }
    }
}
