using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelper
{
    public static void ModifyX(GameObject item, float newX)
    {
        Vector3 itemPos = item.transform.position;
        Vector3 newPos = new Vector3(newX, itemPos.y, itemPos.z);
        item.transform.position = newPos;
    }

    public static void ModifyY(GameObject item, float newY)
    {
        Vector3 itemPos = item.transform.position;
        Vector3 newPos = new Vector3(itemPos.x, newY, itemPos.z);
        item.transform.position = newPos;
    }

    public static void ModifyZ(GameObject item, float newZ)
    {
        Vector3 itemPos = item.transform.position;
        Vector3 newPos = new Vector3(itemPos.x, itemPos.y, newZ);
        item.transform.position = newPos;
    }
}
